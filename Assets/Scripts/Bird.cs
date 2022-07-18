using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("Launch")]
    [SerializeField] private float maxLaunchForce = 500;
    [SerializeField] private float maxDragDistance = 5;
    [SerializeField] private float minDragDistance = 2;

    [Header("Reset")]
    [SerializeField] private float maxVelocityForReset = .3f;
    [SerializeField] private float slowBirdTimeForReset = 3f;
    [SerializeField] private float totalTimeForReset = 10f;

    protected Vector2 _startPosition;
    protected Rigidbody2D _rigidbody2D;
    protected SpriteRenderer _spriteRenderer;

    public bool Interactable { get; set; } = true;

    protected bool isFlying;
    protected bool effectUsed;
    protected bool crashed;

    protected float slowResetTimer;
    protected float totalResetTimer;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!crashed)
        {
            return;
        }

        totalResetTimer += Time.fixedDeltaTime;

        if(totalResetTimer > totalTimeForReset)
        {
            ResetBird();
        }

        if (_rigidbody2D.velocity.sqrMagnitude < maxVelocityForReset * maxVelocityForReset)
        {
            slowResetTimer += Time.fixedDeltaTime;
            if (slowResetTimer > slowBirdTimeForReset)
            {
                ResetBird();
            }
        }
        else
        {
            slowResetTimer = 0;
        }
    }

    void OnMouseDown()
    {
        if (!Interactable)
        {
            return;
        }
        _spriteRenderer.color = Color.red;
    }

    void OnMouseDrag()
    {
        if (!Interactable)
        {
            return;
        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * maxDragDistance);
        }

        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;

        _rigidbody2D.position = desiredPosition;
    }

    void OnMouseUp()
    {
        if (!Interactable)
        {
            return;
        }

        isFlying = true;
        Interactable = false;
        _spriteRenderer.color = Color.white;

        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;

        if (direction.sqrMagnitude < minDragDistance * minDragDistance)
        {
            Interactable = true;
            _rigidbody2D.position = _startPosition;
            return;
        }

        float dragAmount = Vector2.Distance(currentPosition, _startPosition);
        float finalLaunchForce = (maxLaunchForce * dragAmount) / maxDragDistance;

        direction.Normalize();

        Vector2 forceToApply = direction * finalLaunchForce;

        GameController.Instance.SlingShot.StartDragAnimation(forceToApply);

    }

    public void LaunchBird(Vector2 forceToApply)
    {
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(forceToApply);
    }

    public virtual bool ApplyBirdEffect()
    {
        return isFlying && !effectUsed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("StageLimit"))
        {
            ResetBird();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isFlying = false;
        effectUsed = false;
        crashed = true;
    }

    private void ResetBird()
    {
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.SetRotation(0);

        crashed = false;
        Interactable = true;
        totalResetTimer = 0;
        GameController.Instance.RemoveLife();
        GameController.Instance.SlingShot.Launched = false;
        CameraController.Instance.SetCameraTransition(0, 0);
    }
}
    

