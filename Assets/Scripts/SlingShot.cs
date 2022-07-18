using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    [Header("Launch")]
    [SerializeField] private float launchTime = .25f;

    [SerializeField] private Vector3 minusFront;
    [SerializeField] private Vector3 minusBehind;

    [SerializeField] private LineRenderer frontLine;
    [SerializeField] private LineRenderer backLine;

    private Bird _bird;
    private Vector2 _initialPos;

    [SerializeField] private Transform RestingPoint;
    public bool Launched { get; set; }

    private void Start()
    {
        _bird = GameController.Instance.Bird;
        _initialPos = _bird.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Launched)
        {
            return;
        }

        Vector3 slingPoint = _bird.transform.position;

        frontLine.SetPosition(1, slingPoint - frontLine.transform.position - minusFront);
        backLine.SetPosition(1, slingPoint - backLine.transform.position - minusBehind);
    }

    public void StartDragAnimation(Vector2 finalVelocity)
    {
        _bird.transform.LeanMove(_initialPos, launchTime)
            .setEaseInCubic()
            .setOnComplete(_ => {
                Launched = true;
                _bird.LaunchBird(finalVelocity);
                CameraController.Instance.SetCameraTransition(1);
            });
    }

    public Vector2 RestingPosition()
    {
        return (Vector2)RestingPoint.position;
    }
}
