using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MovableObject
{
    private Gun _parent;

    public float Rotation { get; private set; }

    public event Action Hiting;

    public Bullet(Player player, Vector2 position, Vector2 direction, float lostDistance)
        : base(player, position, direction, 7, lostDistance)
    {
        _parent = player.Gun;
        Rotation = player.Rotation;
    }

    public void Hit()
    {
        Hiting?.Invoke();
    }
}
