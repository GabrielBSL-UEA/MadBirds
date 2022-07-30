using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Bird Bird { get; private set; }
    public SlingShot SlingShot { get; private set; }

    [SerializeField] private int lifes;
    [SerializeField] private string music;

    private List<Monster> _monsters = new List<Monster>();
    private int currentLifes;

    private int m_CurrentPoints;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Bird = FindObjectOfType<Bird>();
        SlingShot = FindObjectOfType<SlingShot>();

        currentLifes = lifes;

    }

    private void Start()
    {
        UIController.Instance.SetHearts(lifes);
        SceneManager.sceneLoaded += OnLevelLoaded;

        AudioManager.instance.PlayMusic(music);
    }

    public void AddMonster(Monster newMonster)
    {
        _monsters.Add(newMonster);
    }

    public void RemoveMonster(Monster monsterToRemove)
    {
        m_CurrentPoints += 200;

        _monsters.Remove(monsterToRemove);
        UIController.Instance.SetPointText(m_CurrentPoints);

        if(_monsters.Count == 0)
        {
            m_CurrentPoints += 1000 * currentLifes;
            UIController.Instance.SetPointText(m_CurrentPoints);
            GoToNextLevel();
        }
    }

    public void RemoveLife()
    {
        currentLifes--;

        if(currentLifes >= 0)
        {
            UIController.Instance.RemoveHeart();
            return;
        }

        Time.timeScale = 0;
        UIController.Instance.ActivateDefeatCanvas();
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1;
        m_CurrentPoints = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetScore()
    {
        return m_CurrentPoints;
    }

    void GoToNextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex + 1 == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Victory"))
        {
            FindObjectOfType<HighscoreManager>().Points = m_CurrentPoints;
            Instance = null;
            Destroy(gameObject);
            return;
        }

        currentLifes = 3;
        UIController.Instance.SetHearts(currentLifes);
        UIController.Instance.SetPointText(m_CurrentPoints);

        Bird = FindObjectOfType<Bird>();
        SlingShot = FindObjectOfType<SlingShot>();

        if (scene.name.Equals("Level5"))
        {
            AudioManager.instance.PlayMusic("GameplayTwoMusic");
            return;
        }

        AudioManager.instance.PlayMusic("GameplayOneMusic");
        return;
    }
}
