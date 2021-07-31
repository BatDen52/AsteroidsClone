using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPresenter
{
    private BulletView _view;
    private Bullet _model;

    public BulletPresenter(BulletView view, Bullet model)
    {
        _view = view;
        _model = model;
    }

    public void Enable()
    {
        _view.Moving += OnMoving;

        _model.Hiting += OnHiting;
        _model.Lost += OnLost;
        _model.PositionChenged += OnPositionChenged;
    }

    public void Disable()
    {
        _view.Moving -= OnMoving;

        _model.Hiting -= OnHiting;
        _model.Lost -= OnLost;
        _model.PositionChenged -= OnPositionChenged;
    }

    private void OnMoving(float deltaTime)
    {
        _model.Move(deltaTime);
    }

    private void OnHiting()
    {
    }

    private void OnLost()
    {
        _view.Destroy();
    }

    private void OnPositionChenged(Vector2 position)
    {
        _view.SetPosition(position);
    }
}