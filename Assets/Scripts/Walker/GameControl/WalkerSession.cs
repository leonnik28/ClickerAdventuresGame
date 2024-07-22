using UnityEngine;
using Zenject;

public class WalkerSession : MonoBehaviour
{
    public WalkerData WalkerData => _walkerData;
    public float CurrentTime => _currentTime;

    [SerializeField] private PlayerSpawn _playerSpawn;
    [SerializeField] private GameObject _winUI;

    private WalkerData _walkerData;
    private float _startTime;
    private float _currentTime;

    private readonly string _saveKey = "WalkerData";

    private StorageService _storageService;

    [Inject]
    private void Construct(StorageService storageService)
    {
        _storageService = storageService;
    }

    private async void OnEnable()
    {
        _walkerData = await _storageService.LoadAsync<WalkerData>(_saveKey);
        FinishTarget.OnFinishTrigger += FinishGame;
        StartGame();
    }

    private void OnDisable()
    {
        FinishTarget.OnFinishTrigger -= FinishGame;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;

        _startTime = Time.time;
        _currentTime = 0f;
    }

    private async void FinishGame()
    {
        Time.timeScale = 0f;
        _currentTime = Time.time - _startTime;
        CheckTime();
        _walkerData.Score++;

        await _storageService.SaveAsync(_saveKey, _walkerData);
        _winUI.SetActive(true);
    }

    private void CheckTime()
    {
        if (_walkerData.BestTime == 0)
        {
            _walkerData.BestTime = _currentTime;
            return;
        }
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
