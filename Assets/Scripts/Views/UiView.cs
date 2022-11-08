using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiView : MonoBehaviour
{
    [SerializeField] private Text _score;
    [SerializeField] private Text _coordinates;
    [SerializeField] private Text _rotationAngle;
    [SerializeField] private Text _speed;
    [SerializeField] private Text _laserCharges;
    [SerializeField] private Text _laserRollbackTime;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private Text _gameOverScore;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(Restart);
    }

    public void UpdateScore(int value)
    {
        _score.text = $"Score: {value}.";
        _gameOverScore.text = $"Score: {value}.";
    }

    public void UpdateCoordinates(Vector2 position)
    {
        _coordinates.text = $"X: {position.x}; Y: {position.y}.";
    }

    public void UpdateRotationAngle(int value)
    {
        _rotationAngle.text = $"Angle: {value}.";
    }

    public void UpdateSpeed(float value)
    {
        _speed.text = $"Speed: {value}.";
    }

    public void UpdateLaserCharges(int value)
    {
        _laserCharges.text = $"LaserCharges: {value}.";
    }

    public void UpdateLaserRollbackTime(float time)
    {
        _laserRollbackTime.text = $"LaserRollbackTime: {time}.";
    }

    public void GameOver()
    {
        _gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
