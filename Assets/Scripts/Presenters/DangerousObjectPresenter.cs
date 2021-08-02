using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousObjectPresenter
{
    private DangerousObjectView _view;
    private DangerousObject _model;

    public Vector2 Position => _model.Position;

    public event Action<DangerousObjectPresenter> Lost;
    public event Action<DangerousObjectPresenter> Hiting;
    public event Action GameOver;
    public event Action<DangerousObjectPresenter> SplitAsteriod;

    public DangerousObjectPresenter(DangerousObjectView view, DangerousObject model)
    {
        _view = view;
        _model = model;
    }

    public void Enable()
    {
        _model.PositionChenged += OnPositionChenged;
        _model.Lost += OnLost;

        _view.Moving += OnMoving;
        _view.HitDetected += OnHitDetected;
        _view.LaserHitDetected += OnLaserHitDetected;
        _view.GameOver += OnGameOver;
    }

    public void Disable()
    {
        _model.PositionChenged -= OnPositionChenged;
        _model.Lost -= OnLost;

        _view.Moving -= OnMoving;
        _view.HitDetected -= OnHitDetected;
        _view.LaserHitDetected -= OnLaserHitDetected;
        _view.GameOver -= OnGameOver;

        if(_view?.gameObject)
        _view?.Destroy();
    }

    private void OnPositionChenged(Vector2 position)
    {
        _view.SetPosition(position);
    }

    private void OnMoving(float deltaTime)
    {
        _model.Move(deltaTime);
    }

    private void OnLost(MovableObject movableObject)
    {
        Lost?.Invoke(this);
        _view.Destroy();
    }

    private void OnHitDetected()
    {
        if (_model is Asteroid)
        {
            ((Asteroid)_model).Split();
            SplitAsteriod?.Invoke(this);
        }   

        _model.Die();

        Hiting?.Invoke(this);
    }

    private void OnLaserHitDetected()
    {
        _model.Die();
        Hiting?.Invoke(this);
    }

    private void OnGameOver()
    {
        GameOver?.Invoke();
    }
}
