using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private WalkerSession _walkerSession;
    [SerializeField] private PlayerSpawn _playerSpawn;
    [SerializeField] private GameObject _mainUI;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _topTimeText;
    [SerializeField] private Text _currentTimeText;

    private GameObject _player;

    private void OnEnable()
    {
        _player = _playerSpawn.Player;
        Destroy(_player);

        _scoreText.text = _walkerSession.WalkerData.Score.ToString();
        _topTimeText.text = "Top Time: " + _walkerSession.WalkerData.BestTime.ToString();
        _currentTimeText.text = "Current Time: " + _walkerSession.CurrentTime.ToString();
    }

    public void RestartGame()
    {
        _playerSpawn.SpawnPlayer();
        _walkerSession.StartGame();
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        _walkerSession.gameObject.SetActive(false);
        gameObject.SetActive(false);
        _mainUI.SetActive(true);
    }
}
