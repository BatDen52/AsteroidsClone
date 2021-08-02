using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Laser
{
    private float _workTime;
    private float _time;
    private List<LaserCharge> _charges;

    public int ChargeCount => _charges.Where(x => x.IsReady).Count();
    public bool IsActive { get; private set; }

    public event Action Active;
    public event Action Inactive;
    public event Action ChargeCountChanged;

    public Laser(float cooldown, float workTime, int chargeCount)
    {
        _workTime = workTime;
        IsActive = false;
        _time = 0;
        _charges = new List<LaserCharge>();
        for (int i = 0; i < chargeCount; i++)
        {
            LaserCharge charge = new LaserCharge(cooldown);
            charge.Ready += OnReady;
            _charges.Add(charge);
        }
        ChargeCountChanged?.Invoke();
    }

    public void Disable()
    {
        foreach (var charge in _charges)
            charge.Ready -= OnReady;
    }

    public void TryActive()
    {
        if (ChargeCount > 0)
        {
            _time = 0;
            _charges.First(x => x.IsReady).Shoot();
            IsActive = true;
            Active?.Invoke();
            ChargeCountChanged?.Invoke();
        }
    }

    public void OnReady()
    {
        ChargeCountChanged?.Invoke();
    }

    public void Tick(float deltaTime)
    {
        if (IsActive)
        {
            _time += deltaTime;
            if (_time > _workTime)
            {
                IsActive = false;
                Inactive?.Invoke();
            }
        }
        foreach (var charge in _charges)
        {
            charge.Tick(deltaTime);
        }
    }
}
