using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    [SerializeField] private Vector3 minusFront;
    [SerializeField] private Vector3 minusBehind;

    [SerializeField] private LineRenderer frontLine;
    [SerializeField] private LineRenderer backLine;

    private Bird bird;
    private Vector2 initialPos;

    void Awake()
    {
        bird = FindObjectOfType<Bird>();
        initialPos = bird.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 slingPoint = bird.transform.position;

        if(bird.transform.position.x > initialPos.x)
        {
            slingPoint = initialPos;
        }

        frontLine.SetPosition(1, slingPoint - frontLine.transform.position - minusFront);
        backLine.SetPosition(1, slingPoint - backLine.transform.position - minusBehind);
    }
}
