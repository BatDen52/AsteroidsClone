using System;
using UnityEngine;

public class Asteroid : DangerousObject
{
    public event Action Splited;

    public Asteroid(Player target, Vector2 position, Vector2 direction, float lostDistance) :
        base(target, position, direction, 2, lostDistance, 100){ }

    public void Split()
    {
        Splited?.Invoke();
    }
}
