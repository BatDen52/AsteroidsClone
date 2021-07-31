using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerousObjectPresenter
{
    private DangerousObjectView _view;
    private DangerousObject _model;

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
    }

    public void Disable()
    {
        _model.PositionChenged -= OnPositionChenged;
        _model.Lost -= OnLost;

        _view.Moving -= OnMoving;
    }

    private void OnPositionChenged(Vector2 position)
    {
        _view.SetPosition(position);
    }

    private void OnMoving(float deltaTime)
    {
        _model.Move(deltaTime);
    }

    private void OnLost()
    {
        _view.Destroy();
    }
}
