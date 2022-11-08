using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private SpawnerView _spawnerView;
    [SerializeField] private UiView _uiView;

    private PlayerPresenter _playerPresenter;
    private WeaponPresenter _weaponPresenter;
    private SpawnerPresenter _spawnerPresenter;
    private UiPresenter _uiPresenter;
    private Player _playerModel;
    private Spawner _spawnerModel;
    private Score _scoreModel;

    private void Awake()
    {
        Vector2 leftBottom = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        Space space = new Space(0.5f, leftBottom.y, topRight.y, leftBottom.x, topRight.x);

        _playerModel = new Player(space, 5, 3, 50);
        _playerPresenter = new PlayerPresenter(_playerView, _playerModel);

        _weaponPresenter = new WeaponPresenter(_playerView, _playerModel);

        _spawnerModel = new Spawner(_playerModel,space, 1, 5);
        _spawnerPresenter = new SpawnerPresenter(_spawnerView, _spawnerModel);

        _scoreModel = new Score();
        _uiPresenter = new UiPresenter(_playerModel,_scoreModel,_uiView);
    }

    private void OnEnable()
    {
        _playerPresenter.Enable();
        _weaponPresenter.Enable();
        _spawnerPresenter.Enable();
        _uiPresenter.Enable();
    }

    private void OnDisable()
    {
        _playerPresenter.Disable();
        _weaponPresenter.Disable();
        _spawnerPresenter.Disable();
        _uiPresenter.Disable();
    }
}
