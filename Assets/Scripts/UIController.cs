using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("HUD")]
    [SerializeField] private GameObject heartObject;
    [SerializeField] private Transform heartGroup;

    [Header("Pause Canvas")]
    [SerializeField] private GameObject loseCanvas;

    private void Awake()
    {
        Instance = this;

        loseCanvas.SetActive(false);
    }

    public void SetHearts(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(heartObject, heartGroup.transform);
        }
    }

    public void RemoveHeart()
    {
        Destroy(heartGroup.transform.GetChild(heartGroup.transform.childCount - 1).gameObject);
    }

    public void ActivateDefeatCanvas()
    {
        loseCanvas.SetActive(true);
    }

    public void InformLevelReload()
    {
        GameController.Instance.ReloadLevel();
    }
}
