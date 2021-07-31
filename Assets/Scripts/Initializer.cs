using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private SpawnerView _spawnerView;

    private PlayerPresenter _playerPresenter;
    private SpawnerPresenter _spawnerPresenter;
    private Player _playerModel;
    private Spawner _spawnerModel;

    private void Awake()
    {
        Vector2 leftBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Space space = new Space(0.5f, leftBottom.y, topRight.y, leftBottom.x, topRight.x);

        _playerModel = new Player(space, 5, 3, 50);
        _playerPresenter = new PlayerPresenter(_playerView, _playerModel);

        _spawnerModel = new Spawner(_playerModel,space, 1, 5);
        _spawnerPresenter = new SpawnerPresenter(_spawnerView, _spawnerModel);
    }

    private void OnEnable()
    {
        _playerPresenter.Enable();
        _spawnerPresenter.Enable();
    }

    private void OnDisable()
    {
        _playerPresenter.Disable();
        _spawnerPresenter.Disable();
    }
}
