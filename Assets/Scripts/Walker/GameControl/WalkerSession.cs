using UnityEngine;

public class WalkerSession : MonoBehaviour
{
    public WalkerData WalkerData => _walkerData;
    public float CurrentTime => _currentTime;

    [SerializeField] private PlayerSpawn _playerSpawn;
    [SerializeField] private GameObject _winUI;

    private WalkerData _walkerData;
    private float _startTime;
    private float _currentTime;

    private void OnEnable()
    {
        FinishTarget.OnFinishTrigger += FinishGame;
        StartGame();
    }

    private void OnDisable()
    {
        FinishTarget.OnFinishTrigger -= FinishGame;
    }

    public void StartGame()
    {
        _playerSpawn.SpawnPlayer();
        Time.timeScale = 1f;

        _startTime = Time.time;
        _currentTime = 0f;
    }

    private void FinishGame()
    {
        Time.timeScale = 0f;
        _currentTime = Time.time - _startTime;
        CheckTime();
        _walkerData.Score++;
        _winUI.SetActive(true);
    }

    private void CheckTime()
    {
        if (_currentTime < _walkerData.BestTime)
        {
            _walkerData.BestTime = _currentTime;
        }
    }
}

public struct WalkerData
{
    public float BestTime;
    public int Score;
}
