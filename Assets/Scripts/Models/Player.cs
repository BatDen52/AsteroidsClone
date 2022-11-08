using System;
using UnityEngine;

public class Player
{
    private Vector2 _position;
    private float _rotation;

    private Vector2 _direction;

    private float _moveSpeed;
    private float _maxMoveSpeed;
    private float _acceleration;
    private float _rotateSpeed;

    private Space _space;

    public Vector2 Position
    {
        get => _position;
        private set
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
    public Vector2 Direction => _direction;
    public Gun Gun { get; private set; }
    public Laser Laser { get; private set; }
    public float Rotation
    {
        get => _rotation;
        private set
        {
            _rotation = value % 360;

            _direction.x = Mathf.Cos(Rotation * Mathf.PI / 180);
            _direction.y = Mathf.Sin(Rotation * Mathf.PI / 180);
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
    public event Action Died;
    public event Action<int> RewardTaking;

    public Player(Space space, float maxMoveSpeed, float acceleration, float rotateSpeed)
    {
        Gun = new Gun(this);
        Laser = new Laser(15, 3, 3);
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

    public void Accelerate(float deltaTime)
    {
        MoveSpeed += _acceleration * deltaTime;
        SpeedChenged?.Invoke(MoveSpeed);
    }

    public void Move(float deltaTime)
    {
        Position += _direction * MoveSpeed * deltaTime;
        PositionChenged?.Invoke(Position);
        Brake(deltaTime);
    }

    private void Brake(float deltaTime)
    {
        MoveSpeed -= _space.BrakingForce * deltaTime;
        SpeedChenged?.Invoke(MoveSpeed);
    }

    public void Shoot()
    {
        Gun.Shoot(_space);
    }

    public void ShootLaser()
    {
        Laser.TryActive();
    }

    public void TakeReward(int reward)
    {
        RewardTaking?.Invoke(reward);
    }

    public void Die()
    {
        Laser.Disable();
        Died?.Invoke();
    }
}