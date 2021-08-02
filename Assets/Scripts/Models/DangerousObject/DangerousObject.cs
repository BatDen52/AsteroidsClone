using System;
using UnityEngine;

public abstract class DangerousObject : MovableObject
{
    public int Reward { get; private set; }

    public DangerousObject(Player target, Vector2 position, Vector2 direction, float moveSpeed, float lostDistance, int reward)
        : base(target, position, direction, moveSpeed, lostDistance)
    {
        Reward = reward;
    }

    public virtual void Die()
    {
        Target.TakeReward(Reward);
    }
}