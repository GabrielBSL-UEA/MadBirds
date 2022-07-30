using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    [Header("Scenario")]
    [SerializeField] private Vector3 scenarioPosition;
    [SerializeField] private float scenarioSize;

    [Header("Birds")]
    [SerializeField] private Vector3 birdsPosition;
    [SerializeField] private float birdsSize;

    [Header("Transitions")]
    [SerializeField] private float transitionsTime;
    [SerializeField] private LeanTweenType transitionType;
    [SerializeField] private float firstTransitionDelay;

    private Vector3[] positions = new Vector3[2];
    private float[] sizes = new float[2];

    private void Awake()
    {
        Instance = this;

        positions[0] = scenarioPosition;
        positions[1] = birdsPosition;

        sizes[0] = scenarioSize;
        sizes[1] = birdsSize;
    }

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera.transform.position = scenarioPosition;
        virtualCamera.m_Lens.OrthographicSize = scenarioSize;

        SetCameraTransition(0, firstTransitionDelay);
    }

    /*
     * transitionValue:
     *  0 -> scenario to birds
     *  1 -> birds to scenario
     */
    public void SetCameraTransition(int transitionValue, float delay = 0f)
    {
        LeanTween.cancel(gameObject);

        LeanTween.value(0, 1, transitionsTime)
            .setOnUpdate(value =>
            {
                if(virtualCamera == null)
                {
                    return;
                }
                virtualCamera.transform.position = Vector3.Lerp(positions[transitionValue % 2], positions[(transitionValue + 1) % 2], value);
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(sizes[transitionValue % 2], sizes[(transitionValue + 1) % 2], value);
            })
            .setEase(transitionType)
            .setDelay(delay);
    }

    private void OnDestroy()
    {
        LeanTween.cancel(gameObject);
    }
}
