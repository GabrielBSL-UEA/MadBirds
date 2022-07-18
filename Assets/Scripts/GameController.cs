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

    private List<Monster> _monsters = new List<Monster>();
    private int currentLifes;

    private void Awake()
    {
        Instance = this;

        Bird = FindObjectOfType<Bird>();
        SlingShot = FindObjectOfType<SlingShot>();

        currentLifes = lifes;
    }

    private void Start()
    {
        UIController.Instance.SetHearts(lifes);
    }

    public void AddMonster(Monster newMonster)
    {
        _monsters.Add(newMonster);
    }

    public void RemoveMonster(Monster monsterToRemove)
    {
        _monsters.Remove(monsterToRemove);

        if(_monsters.Count == 0)
        {
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        LeanTween.cancelAll();
    }
}
