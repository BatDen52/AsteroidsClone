using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidTemplate;
    [SerializeField] private GameObject _fragmentTemplate;
    [SerializeField] private GameObject _ufoTemplate;

    public event Action<DangerousObjectView> ViewCreated;
    public event Action<float> Tikc;

    public void CreateAsteroidView() => CreateDangerousObjectView(_asteroidTemplate);

    public void CreateFragmentView() => CreateDangerousObjectView(_fragmentTemplate);

    public void CreateUfoView() => CreateDangerousObjectView(_ufoTemplate);

    private void CreateDangerousObjectView(GameObject template)
    {
        DangerousObjectView view = Instantiate(template, transform).GetComponent<DangerousObjectView>();
        ViewCreated?.Invoke(view);
    }

    private void Update()
    {
        Tikc?.Invoke(Time.deltaTime);
    }
}
