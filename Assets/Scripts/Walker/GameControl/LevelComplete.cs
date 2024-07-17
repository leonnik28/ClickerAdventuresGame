using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] private WalkerSession _walkerSession;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _mainUI;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _topTimeText;
    [SerializeField] private Text _currentTimeText;

    private void OnEnable()
    {
        _player.SetActive(false);

        _scoreText.text = _walkerSession.WalkerData.Score.ToString();
        _topTimeText.text = "Top Time: " + _walkerSession.WalkerData.BestTime.ToString();
        _currentTimeText.text = "Current Time: " + _walkerSession.CurrentTime.ToString();
    }

    public void RestartGame()
    {
        _walkerSession.StartGame();
        _player.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        _walkerSession.gameObject.SetActive(false);
        gameObject.SetActive(false);
        _mainUI.SetActive(true);
    }
}
