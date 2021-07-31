using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidTemplate;
    [SerializeField] private GameObject _fragmentTemplate;
    [SerializeField] private GameObject _ufoTemplate;
    private Transform _transform;

    public event Action<DangerousObjectView> ViewCreated;
    public event Action<float> Tikc;

    private void OnEnable()
    {
        _transform = transform;
    }

    public void CreateAsteroidView()
    {
        DangerousObjectView  view = Instantiate(_asteroidTemplate, _transform).GetComponent<DangerousObjectView>();
        ViewCreated?.Invoke(view);
    }

    public void CreateFragmentView()
    {
        DangerousObjectView  view = Instantiate(_fragmentTemplate, _transform).GetComponent<DangerousObjectView>();
        ViewCreated?.Invoke(view);
    }

    public void CreateUfoView()
    {
        DangerousObjectView  view = Instantiate(_ufoTemplate, _transform).GetComponent<DangerousObjectView>();
        ViewCreated?.Invoke(view);
    }

    private void Update()
    {
        Tikc?.Invoke(Time.deltaTime);
    }
}
