using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [Header("HUD")]
    [SerializeField] private GameObject heartObject;
    [SerializeField] private Transform heartGroup;
    [SerializeField] private TextMeshProUGUI pointsText;

    [Header("Pause Canvas")]
    [SerializeField] private GameObject loseCanvas;

    private void Awake()
    {
        Instance = this;

        loseCanvas.SetActive(false);
    }

    public void SetHearts(int amount)
    {
        foreach (Transform item in heartGroup)
        {
            Destroy(item.gameObject);
        }

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

    public void SetPointText(int points)
    {
        pointsText.text = points.ToString();
    }
}
