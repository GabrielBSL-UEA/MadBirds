using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBird : Bird
{
    [Header("SpeedUp Special")]
    [SerializeField] private float speedUpForce;

    public override bool ApplyBirdEffect()
    {
        if (!base.ApplyBirdEffect())
        {
            return false;
        }

        effectUsed = true;
        Vector2 currentDirection = _rigidbody2D.velocity.normalized;

        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce(currentDirection * speedUpForce);

        return true;
    }
}
