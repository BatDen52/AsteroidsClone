using System;
using UnityEngine;

public class DangerousObjectView : MovableObjectView
{
    public event Action HitDetected;
    public event Action LaserHitDetected;
    public event Action GameOver;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out BulletView bullet))
        {
            Destroy(gameObject);
            HitDetected?.Invoke();
        }
        if (collision.TryGetComponent(out LaserView laser))
        {
            Destroy(gameObject);
            LaserHitDetected?.Invoke();
        }
        if (collision.TryGetComponent(out PlayerView player))
        {
            GameOver?.Invoke();
        }
    }
}
