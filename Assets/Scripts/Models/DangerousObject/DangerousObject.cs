using System;
using UnityEngine;

public abstract class DangerousObject : MovableObject
{
    public DangerousObject(Player target, Vector2 position, Vector2 direction, float moveSpeed, float lostDistance)
        :base(target, position, direction, moveSpeed, lostDistance){}
}