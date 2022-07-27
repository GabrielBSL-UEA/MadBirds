using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTReactor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer blinkSprite;
    [SerializeField] private GameObject explosion;

    [SerializeField] private float blinkFrequence;
    [SerializeField] private float timeToExplode;

    private bool explosionSequenceStarted;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (explosionSequenceStarted)
        {
            return;
        }

        Bird bird = collision.gameObject.GetComponent<Bird>();
        if (bird != null)
        {
            StartCoroutine(ExplosionSequence());
        }
       
    }

    IEnumerator ExplosionSequence()
    {
        explosionSequenceStarted = true;
        StartCoroutine(BlinkingSequence());

        yield return new WaitForSeconds(timeToExplode);

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        FindObjectOfType<AudioManager>().PlaySFX("Explosion");
    }

    IEnumerator BlinkingSequence()
    {
        while (true)
        {
            blinkSprite.enabled = !blinkSprite.enabled;
            yield return new WaitForSeconds(blinkFrequence);
        }
    }
}
