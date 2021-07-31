using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : DangerousObject
{
    public Fragment(Player target, Vector2 position, Vector2 direction, float lostDistance) :
        base(target, position, direction, 3, lostDistance) { }
}
