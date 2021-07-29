using UnityEngine;

public class PlayerPresenter
{
    private PlayerView _view;
    private Player _model;

    public PlayerPresenter(PlayerView view, Player model)
    {
        _view = view;
        _model = model;
    }

    public void Enable()
    {
        _model.RotationChenged += OnRotationChenged;
        _model.PositionChenged+= OnPositionChenged;
        _model.SpeedChenged+= OnSpeedChenged;

        _view.Rotating += OnRotating;
        _view.Accelerating += OnAccelerating;
        _view.Moving += OnMoving;
    }

    public void Disable()
    {
        _model.RotationChenged -= OnRotationChenged;
        _model.PositionChenged -= OnPositionChenged;
        _model.SpeedChenged -= OnSpeedChenged;

        _view.Rotating -= OnRotating;
        _view.Accelerating -= OnAccelerating;
        _view.Moving -= OnMoving;
    }

    private void OnRotationChenged(float rotation)
    {
        _view.SetRotation(rotation);
    }

    private void OnPositionChenged(Vector2 position)
    {
        _view.SetPosition(position);
    }

    private void OnSpeedChenged(float speed)
    {

    }

    private void OnRotating(int direction, float deltaTime)
    {
        _model.Rotate(direction, deltaTime);
    }

    private void OnAccelerating()
    {
        _model.Accelerate();
    }

    private void OnMoving(float deltaTime)
    {
        _model.Move(deltaTime);
    }
}
