using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser
{
    public event Action LaserActive;

    public void Active()
    {
        LaserActive?.Invoke();
    }
}
