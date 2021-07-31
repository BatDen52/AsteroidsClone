using System;
using UnityEngine;

public class DangerousObjectView : MonoBehaviour
{
    public event Action<float> Moving;

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        Moving?.Invoke(Time.deltaTime);
    }
}
