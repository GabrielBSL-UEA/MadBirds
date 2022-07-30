using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDetector : MonoBehaviour, IPointerDownHandler
{
    private Bird birdToEffect;

    private void Awake()
    {
        birdToEffect = FindObjectOfType<Bird>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        birdToEffect.ApplyBirdEffect();
    }
}
