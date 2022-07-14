using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce;
    [SerializeField] float _maxDragDistance;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    private Vector2 _startPosition;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = rigidbody2D.position;
        rigidbody2D.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());

    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        rigidbody2D.position = _startPosition;
        rigidbody2D.isKinematic = true;
        rigidbody2D.velocity = Vector2.zero;
    }
    #region MouseSetUp
    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }

        if (desiredPosition.x > _startPosition.x)
            desiredPosition.x = _startPosition.x;

        rigidbody2D.position = desiredPosition;
    }
    private void OnMouseDown()
    {
        spriteRenderer.color = Color.red;
    }
    private void OnMouseUp()
    {
        Vector2 currentPosition = rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        rigidbody2D.isKinematic = false;
        rigidbody2D.AddForce(direction * _launchForce);

        spriteRenderer.color = Color.white;
    }
}
    #endregion

