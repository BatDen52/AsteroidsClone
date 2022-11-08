using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPresenter
{
    private SpawnerView _view;
    private Spawner _model;
    private DangerousObject _dangerousObjectModel;
    private List<DangerousObjectPresenter> _dangerousObjectPresenters;

    public SpawnerPresenter(SpawnerView view, Spawner model)
    {
        _view = view;
        _model = model;
    }

    public void Enable()
    {
        _dangerousObjectPresenters = new List<DangerousObjectPresenter>();
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

        foreach (var presenter in _dangerousObjectPresenters)
        {
            presenter.Lost -= OnLost;
            presenter.Hiting -= OnHiting;
            presenter.GameOver -= OnGameOver;
            presenter.SplitAsteriod -= OnSplit;
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

    private void OnViewCreated(DangerousObjectView dangerousObjectView)
    {
        DangerousObjectPresenter dangerousObjectPresenter = new DangerousObjectPresenter(dangerousObjectView, _dangerousObjectModel);
        dangerousObjectPresenter.Lost += OnLost;
        dangerousObjectPresenter.Hiting += OnHiting;
        dangerousObjectPresenter.GameOver += OnGameOver;
        dangerousObjectPresenter.SplitAsteriod += OnSplit;
        dangerousObjectPresenter.Enable();
        _dangerousObjectPresenters.Add(dangerousObjectPresenter);
    }

    private void OnLost(DangerousObjectPresenter dangerousObjectPresenter)
    {
        dangerousObjectPresenter.Lost -= OnLost;
        _dangerousObjectPresenters.Remove(dangerousObjectPresenter);
    }

    private void OnHiting(DangerousObjectPresenter dangerousObjectPresenter)
    {
        dangerousObjectPresenter.Hiting -= OnHiting;
        _dangerousObjectPresenters.Remove(dangerousObjectPresenter);
    }

    private void OnGameOver()
    {
        Disable();
    }

    private void OnSplit(DangerousObjectPresenter dangerousObjectPresenter)
    {
        dangerousObjectPresenter.SplitAsteriod -= OnSplit;
        _dangerousObjectPresenters.Remove(dangerousObjectPresenter);

        _model.Spawn(3, dangerousObjectPresenter.Position);
    }

    private void OnTick(float deltaTime)
    {
        _model.Spawn(deltaTime);
    }
}
