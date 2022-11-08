using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPresenter
{
    private BulletView _view;
    private Bullet _model;
    
    public event Action<BulletPresenter> Lost;
    public event Action<BulletPresenter> Hiting;

    public BulletPresenter(BulletView view, Bullet model)
    {
        _view = view;
        _model = model;
    }

    public void Enable()
    {
        _view.Moving += OnMoving;
        _view.HitDetected += OnHitDetected;

        _model.Hiting += OnHiting;
        _model.Lost += OnLost;
        _model.PositionChenged += OnPositionChenged;
    }

    public void Disable()
    {
        _view.Moving -= OnMoving;
        _view.HitDetected -= OnHitDetected;

        _model.Hiting -= OnHiting;
        _model.Lost -= OnLost;
        _model.PositionChenged -= OnPositionChenged;
    }

    private void OnMoving(float deltaTime)
    {
        _model.Move(deltaTime);
    }

    private void OnHitDetected()
    {
        _model.Hit();
        Hiting?.Invoke(this);
    }

    private void OnHiting()
    {
    }

    private void OnLost(MovableObject movableObject)
    {
        _view.Destroy();
        Lost?.Invoke(this);
    }

    private void OnPositionChenged(Vector2 position)
    {
        _view.SetPosition(position);
    }
}