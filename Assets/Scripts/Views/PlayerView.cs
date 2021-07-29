using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerInput _playerInput;

    public event Action<int, float> Rotating;
    public event Action Accelerating;
    public event Action<float> Moving;

    private void OnEnable()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    private void OnDisable()
    {
    }

    public void SetRotation(float rotation)
    {
        transform.eulerAngles = new Vector3(0, 0, rotation);
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    private void Update()
    {
        int direcion = (int)_playerInput.Player.Rotation.ReadValue<float>();
        float acceleration = _playerInput.Player.Accelerate.ReadValue<float>();

        if (direcion != 0)
            Rotating?.Invoke(direcion, Time.deltaTime);
        if (acceleration > 0)
            Accelerating?.Invoke();

        Moving?.Invoke(Time.deltaTime);
    }
}
