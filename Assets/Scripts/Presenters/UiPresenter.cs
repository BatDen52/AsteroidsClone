using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPresenter
{
    private Player _playerModel;
    private Score _scoreModel;

    private UiView _view;

    public UiPresenter(Player playerModel, Score scoreModel, UiView view)
    {
        _playerModel = playerModel;
        _scoreModel = scoreModel;
        _view = view;
    }

    public void Enable()
    {
        _scoreModel.ValueCanged += OnValueCanged;

        _playerModel.PositionChenged += OnPositionChenged;
        _playerModel.RotationChenged += OnRotationChenged;
        _playerModel.SpeedChenged += OnSpeedChenged; 
        _playerModel.RewardTaking += OnRewardTaking;
        _playerModel.Died += OnDied;
        _playerModel.Laser.ChargeCountChanged += OnChargeCountChanged;
        _playerModel.Laser.DelayChanged += OnDelayChanged;

        OnValueCanged(0);
        OnPositionChenged(_playerModel.Position);
        OnRotationChenged(_playerModel.Rotation);
        OnSpeedChenged(0); 
        OnChargeCountChanged(_playerModel.Laser.ChargeCount);
        OnDelayChanged(0);
    }

    public void Disable()
    {
        _scoreModel.ValueCanged -= OnValueCanged;

        _playerModel.PositionChenged -= OnPositionChenged;
        _playerModel.RotationChenged -= OnRotationChenged;
        _playerModel.SpeedChenged -= OnSpeedChenged;
        _playerModel.RewardTaking -= OnRewardTaking;
        _playerModel.Died -= OnDied;
        _playerModel.Laser.ChargeCountChanged -= OnChargeCountChanged;
        _playerModel.Laser.DelayChanged -= OnDelayChanged;
    }

    public void OnValueCanged(int value)
    {
        _view.UpdateScore(value);
    }

    public void OnPositionChenged(Vector2 position)
    {
        _view.UpdateCoordinates(position);
    }
    
    public void OnRotationChenged(float value)
    {
        _view.UpdateRotationAngle((int)value);
    }
    
    public void OnSpeedChenged(float value)
    {
        _view.UpdateSpeed(value);
    }
    
    public void OnChargeCountChanged(int value)
    {
        _view.UpdateLaserCharges(value);
    }
    
    public void OnRewardTaking(int value)
    {
        _scoreModel.AddValue(value);
    }

    public void OnDelayChanged(float value)
    {
        _view.UpdateLaserRollbackTime(value);
    }

    public void OnDied()
    {
        _view.GameOver();
    }
}
