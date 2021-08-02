using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPresenter
{
    private PlayerView _view;
    private Player _model;

    private Bullet _bulletModel;
    private List<BulletPresenter> _bulletPresenters;

    public WeaponPresenter(PlayerView view, Player model)
    {
        _view = view;
        _model = model;
    }

    public void Enable()
    {
        _bulletPresenters = new List<BulletPresenter>();
        _model.Gun.BulletCreated += OnBulletCreated;
        _model.Laser.Active += OnLaserActive;
        _model.Laser.Inactive += OnLaserInactive;

        _view.Tick += OnTick;
        _view.Shooting += OnShooting;
        _view.ShootingLaser += OnShootingLaser;
        _view.BulletViewCreated += OnBulletViewCreated;
    }

    public void Disable()
    {
        _model.Gun.BulletCreated -= OnBulletCreated;
        _model.Laser.Active -= OnLaserActive;
        _model.Laser.Inactive -= OnLaserInactive;

        _view.Tick -= OnTick;
        _view.Shooting -= OnShooting;
        _view.ShootingLaser -= OnShootingLaser;
        _view.BulletViewCreated -= OnBulletViewCreated;

        foreach (var bulletPresenter in _bulletPresenters)
        {
            bulletPresenter.Lost -= OnLost;
            bulletPresenter.Hiting -= OnHiting;
            bulletPresenter.Disable();
        }
    }

    public void OnBulletCreated(Bullet bullet)
    {
        _bulletModel = bullet;
        _view.CreateBulletView(bullet.Rotation);
    }

    private void OnBulletViewCreated(BulletView bulletView)
    {
        BulletPresenter bulletPresenter = new BulletPresenter(bulletView, _bulletModel);
        bulletPresenter.Hiting += OnHiting;
        bulletPresenter.Lost += OnLost;
        bulletPresenter.Enable();
        _bulletPresenters.Add(bulletPresenter);
    }

    private void OnLost(BulletPresenter bulletPresenter)
    {
        bulletPresenter.Lost -= OnLost;
        _bulletPresenters.Remove(bulletPresenter);
    }

    private void OnHiting(BulletPresenter bulletPresenter)
    {
        bulletPresenter.Hiting -= OnHiting;
        _bulletPresenters.Remove(bulletPresenter);
    }

    public void OnLaserActive()
    {
        _view.ActiveLaser();
    }

    public void OnLaserInactive()
    {
        _view.InactiveLaser();
    }

    public void OnShooting()
    {
        _model.Shoot();
    }

    public void OnShootingLaser()
    {
        _model.ShootLaser();
    }

    private void OnTick(float deltaTime)
    {
        _model.Laser.Tick(deltaTime);
    }
}
