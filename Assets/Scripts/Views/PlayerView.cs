using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject _bulletTemplate;
    [SerializeField] private LaserView _laser;

    private PlayerInput _playerInput;

    public event Action<int, float> Rotating;
    public event Action<float> Accelerating;
    public event Action<float> Moving;
    public event Action<BulletView> BulletViewCreated;
    public event Action Shooting;
    public event Action ShootingLaser;
    public event Action<float> Tick;
    public event Action Dying;

    private void OnEnable()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Player.ShootGun.performed += ctx => ShootGun();
        _playerInput.Player.ShootLaser.performed += ctx => ShootLaser();
    }

    private void OnDisable()
    {
        _playerInput.Player.ShootGun.performed -= ctx => ShootGun();
        _playerInput.Player.ShootLaser.performed -= ctx => ShootLaser();
        _playerInput.Disable();
    }

    public void Die()
    {
        Dying?.Invoke();
        Destroy(gameObject);
    }

    public void CreateBulletView(float rotation)
    {
        BulletView view = Instantiate(_bulletTemplate, transform).GetComponent<BulletView>();
        view.transform.eulerAngles = new Vector3(0, 0, rotation);
        BulletViewCreated?.Invoke(view);
    }

    public void ShootGun()
    {
        Shooting?.Invoke();
    }

    public void ShootLaser()
    {
        ShootingLaser?.Invoke();
    }

    public void ActiveLaser()
    {
        _laser.gameObject.SetActive(true);
    }

    public void InactiveLaser()
    {
        _laser.gameObject.SetActive(false);
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
            Accelerating?.Invoke(Time.deltaTime);

        Moving?.Invoke(Time.deltaTime);
        Tick?.Invoke(Time.deltaTime);
    }
}
