using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    [SerializeField] private GameObject highscoreLine;
    [SerializeField] private Transform highscoreBox;
    [SerializeField] private TMP_InputField playerNameInputField;

    [SerializeField] private GameObject submitButton;
    [SerializeField] private TextMeshProUGUI finalPointsText;

    private List<HighscoreEntry> scoreData = new List<HighscoreEntry>();

    private string savePath;

    public int Points { get; set; }

    private void Awake()
    {
        savePath = Application.persistentDataPath + "/score.data";
    }
    private void Start()
    {
        finalPointsText.text = "Pontuação Final: " + Points;
        LoadHighscoreData();
    }

    public void LoadHighscoreData()
    {
        if (File.Exists(savePath))
        {
            var binaryFormatter = new BinaryFormatter();

            using (var fileStream = File.Open(savePath, FileMode.Open))
                scoreData = (List<HighscoreEntry>)binaryFormatter.Deserialize(fileStream);
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                var score = new HighscoreEntry()
                {
                    playerName = "XXX",
                    playerScore = 0
                };

                scoreData.Add(score);
            }
        }

        SaveHighscoreData();
        DisplayHighscore();
        CheckPlayerScore();
    }

    public void SaveScoreEntry()
    {
        if (playerNameInputField.text.Length == 0) return;

        var newEntry = new HighscoreEntry()
        {
            playerName = playerNameInputField.text,
            playerScore = Points
        };

        scoreData.Add(newEntry);
        scoreData = scoreData.OrderByDescending(f => f.playerScore).ToList();
        scoreData.RemoveAt(scoreData.Count - 1);

        submitButton.SetActive(false);

        SaveHighscoreData();
        DisplayHighscore();
    }

    private void SaveHighscoreData()
    {
        var binaryFormatter = new BinaryFormatter();

        using (var fileStream = File.Create(savePath))
            binaryFormatter.Serialize(fileStream, scoreData);
    }

    private void DisplayHighscore()
    {
        foreach (Transform oldScore in highscoreBox)
            Destroy(oldScore.gameObject);

        int index = 1;

        foreach (var newScore in scoreData)
        {
            var newHighscoreLine = Instantiate(highscoreLine, highscoreBox);

            newHighscoreLine.transform.GetChild(0).Find("Place").GetComponent<TextMeshProUGUI>().text = index + "º";
            newHighscoreLine.transform.GetChild(0).Find("Score").GetComponent<TextMeshProUGUI>().text = newScore.playerName;
            newHighscoreLine.transform.GetChild(0).Find("Name").GetComponent<TextMeshProUGUI>().text = Mathf.Floor(newScore.playerScore).ToString();

            index++;
        }
    }

    private void CheckPlayerScore()
    {
        if (Points > scoreData[scoreData.Count - 1].playerScore)
        {
            submitButton.SetActive(true);
        }
        else
        {
            submitButton.SetActive(false);
        }
    }
}
