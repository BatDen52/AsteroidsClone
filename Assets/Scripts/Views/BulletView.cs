using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletView : MovableObjectView
{
    public event Action HitDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DangerousObjectView dangerousObject))
        {
            Destroy(gameObject);
            HitDetected?.Invoke();
        }
    }
}
