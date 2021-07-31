using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPresenter
{
    private SpawnerView _view;
    private Spawner _model;
    private DangerousObject _dangerousObjectModel;
    private List<DangerousObjectPresenter> _asteriodPresenters;

    public SpawnerPresenter(SpawnerView view, Spawner model)
    {
        _view = view;
        _model = model;
    }

    public void Enable()
    {
        _asteriodPresenters = new List<DangerousObjectPresenter>();
        _model.Enabled();
        _model.Spawned += OnSpawned;
        _view.ViewCreated += OnViewCreated;
        _view.Tikc += OnTick;
    }

    public void Disable()
    {
        _model.Disable();
        _model.Spawned -= OnSpawned;
        _view.ViewCreated -= OnViewCreated;
        _view.Tikc -= OnTick;

        foreach (var presenter in _asteriodPresenters)
        {
            presenter.Disable();
        }
    }

    private void OnSpawned(DangerousObject dangerousObject)
    {
        _dangerousObjectModel = dangerousObject;
        if (_dangerousObjectModel is Asteroid)
            _view.CreateAsteroidView();
        if (_dangerousObjectModel is Fragment)
            _view.CreateFragmentView();
        if (_dangerousObjectModel is UFO)
            _view.CreateUfoView();
    }

    private void OnViewCreated(DangerousObjectView asteroidView)
    {
        DangerousObjectPresenter asteroidPresenter = new DangerousObjectPresenter(asteroidView, _dangerousObjectModel);
        asteroidPresenter.Enable();
        _asteriodPresenters.Add(asteroidPresenter);
    }

    private void OnTick(float deltaTime)
    {
        _model.Spawn(deltaTime);
    }
}
