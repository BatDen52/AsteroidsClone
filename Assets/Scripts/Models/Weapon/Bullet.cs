using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MovableObject
{
    public float Rotation { get; private set; }

    public event Action Hiting;

    public Bullet(Player target, Vector2 position, Vector2 direction, float lostDistance)
        : base(target, position, direction, 5, lostDistance)
    {
        Rotation = target.Rotation;
    }

    public void Hit()
    {
        Hiting?.Invoke();
    }
}
