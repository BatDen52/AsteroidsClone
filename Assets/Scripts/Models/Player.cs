using System;
using UnityEngine;

public class Player
{
    private Vector2 _position;
    private float _rotation;

    private Vector2 _forwardDirection;

    private float _moveSpeed;
    private float _maxMoveSpeed;
    private float _acceleration;
    private float _rotateSpeed;

    private Space _space;

    private Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            if (_position.y > _space.Top)
                _position.y = _space.Bottom;
            if (_position.y < _space.Bottom)
                _position.y = _space.Top;
            if (_position.x > _space.Right)
                _position.x = _space.Left;
            if (_position.x < _space.Left)
                _position.x = _space.Right;
        }
    }
    private float Rotation
    {
        get => _rotation;
        set
        {
            _rotation = value % 360;

            _forwardDirection.x = Mathf.Cos(Rotation * Mathf.PI / 180);
            _forwardDirection.y = Mathf.Sin(Rotation * Mathf.PI / 180);
        }
    }
    private float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = Mathf.Clamp(value, 0, _maxMoveSpeed);
    }

    public event Action<Vector2> PositionChenged;
    public event Action<float> RotationChenged;
    public event Action<float> SpeedChenged;

    public Player(Space space, float maxMoveSpeed, float acceleration, float rotateSpeed)
    {
        _space = space;
        Position = new Vector2(0, 0);
        Rotation = 0;
        MoveSpeed = 0;
        _maxMoveSpeed = maxMoveSpeed;
        _acceleration = acceleration;
        _rotateSpeed = rotateSpeed;
    }
    public void Rotate(int direction, float deltaTime)
    {
        Rotation += _rotateSpeed * direction * deltaTime;

        RotationChenged?.Invoke(Rotation);
    }

    public void Accelerate()
    {
        MoveSpeed += _acceleration;
        SpeedChenged?.Invoke(MoveSpeed);
    }

    public void Move(float deltaTime)
    {
        Position += _forwardDirection * MoveSpeed * deltaTime;
        PositionChenged?.Invoke(Position);
        Brake(deltaTime);
    }

    private void Brake(float deltaTime)
    {
        MoveSpeed -= _space.BrakingForce * deltaTime;
        SpeedChenged?.Invoke(MoveSpeed);
    }
}
