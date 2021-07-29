using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private PlayerView _view;

    private PlayerPresenter _presenter;
    private Player _model;

    private void Awake()
    {
        Vector2 leftBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Space space = new Space(0.5f, leftBottom.y, topRight.y, leftBottom.x, topRight.x);

        _model = new Player(space, 5, 0.1f, 50);
        _presenter = new PlayerPresenter(_view, _model);
    }

    private void OnEnable()
    {
        _presenter.Enable();
    }

    private void OnDisable()
    {
        _presenter.Disable();
    }
}
