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
        _model.Laser.LaserActive += OnLaserActive;

        _view.Shooting += OnShooting;
        _view.ShootingLaser += OnShootingLaser;
        _view.BulletViewCreated += OnBulletViewCreated;
    }

    public void Disable()
    {
        _model.Gun.BulletCreated -= OnBulletCreated;
        _model.Laser.LaserActive -= OnLaserActive;

        _view.Shooting -= OnShooting;
        _view.ShootingLaser -= OnShootingLaser;
        _view.BulletViewCreated -= OnBulletViewCreated;
    }

    public void OnBulletCreated(Bullet bullet)
    {
        Debug.Log("Shoot");
        _bulletModel = bullet;
        _view.CreateBulletView(bullet.Rotation);
    }

    private void OnBulletViewCreated(BulletView bulletView)
    {
        BulletPresenter bulletPresenter = new BulletPresenter(bulletView, _bulletModel);
        bulletPresenter.Enable();
        _bulletPresenters.Add(bulletPresenter);
    }

    public void OnLaserActive()
    {
        Debug.Log("LaserActive");
    }

    public void OnShooting()
    {
        _model.Shoot();
    }

    public void OnShootingLaser()
    {
        _model.ShootLaser();
    }
}
