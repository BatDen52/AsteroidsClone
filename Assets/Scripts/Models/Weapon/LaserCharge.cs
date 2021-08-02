using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCharge
{
    private float _cooldown;
    private float _delay;

    public bool IsReady { get; private set; }
    public float Delay
    {
        get => _delay;
        private set => _delay = value < 0 ? 0 : value;
    }

    public event Action Ready;

    public LaserCharge(float cooldown)
    {
        _cooldown = cooldown;
        Delay = 0;
        IsReady = true;
    }

    public void Shoot()
    {
        Delay = _cooldown;
        IsReady = false;
    }

    public void Tick(float deltaTime)
    {
        if (IsReady == false)
        {
            Delay -= deltaTime;
            if (Delay <= 0)
            {
                IsReady = true;
                Ready?.Invoke();
            }
        }
    }
}
