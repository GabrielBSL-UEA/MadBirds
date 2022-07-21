using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("Level0");
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credit");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Saiu!");
    }
}
