using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] string _nextLevelName;
    private Monster[] _monsters;

    void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();
    }




    // Update is called once per frame
    void Update()
    {
        if (MonsterAreAllDead())
            GoToNextLevel();
    }

    void GoToNextLevel()
    {
        Debug.Log("Go to level" + _nextLevelName);
        SceneManager.LoadScene(_nextLevelName);
    }

    bool MonsterAreAllDead()
    {
        foreach (var monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }

        return true;
    }

}
