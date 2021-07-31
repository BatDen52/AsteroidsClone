using System;
using UnityEngine;

public abstract class DangerousObject
{
    private float _lostDistance;
    private float _moveSpeed;

    protected Player Target { get; private set; }
    protected Vector2 Direction { get; set; }
    protected Vector2 Position { get; set; }

    public event Action<Vector2> PositionChenged;
    public event Action Lost;

    public DangerousObject(Player target, Vector2 position, Vector2 direction, float moveSpeed, float lostDistance)
    {
        Target = target;
        Position = position;
        Direction = direction;
        _moveSpeed = moveSpeed;
        _lostDistance = lostDistance;
    }

    public virtual void Move(float deltaTime)
    {
        Position += Direction * _moveSpeed * deltaTime;
        PositionChenged?.Invoke(Position);

        float distanceToTarget = (float)Math.Sqrt(
            Math.Pow(Target.Position.x - Position.x, 2) +
            Math.Pow(Target.Position.y - Position.y, 2));
        if (distanceToTarget > _lostDistance)
            Lost?.Invoke();
    }
}