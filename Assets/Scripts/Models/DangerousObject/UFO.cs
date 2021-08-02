using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : DangerousObject
{
    public UFO(Player target, Vector2 position, Vector2 direction, float lostDistance) :
        base(target, position, direction, 2, lostDistance, 250) { }

    public override void Move(float deltaTime)
    {
        base.Move(deltaTime);
        Direction = new Vector2(Target.Position.x - Position.x, Target.Position.y - Position.y).normalized;
    }
}
