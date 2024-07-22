using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ClickerSession : MonoBehaviour
{
    public static Action<int> OnCountClickChange;

    [SerializeField] private MainMenuController _mainMenuController;
    [SerializeField] private Image _clickImage;
    [SerializeField] private Button _clickButton;

    private int _countClick;
    private Texture2D _clickTexture;

    private StorageService _storageService;
    private CloudManager _cloudManager;

    private readonly string _saveKey = "CountClick";

    [Inject]
    private void Construct(StorageService storageService, CloudManager cloudManager)
    {
        _storageService = storageService;
        _cloudManager = cloudManager;
    }

    private async void OnEnable()
    {
        _clickButton.onClick.AddListener(OnClick);
        _countClick = await _storageService.LoadAsync<int>(_saveKey);
        OnCountClickChange?.Invoke(_countClick);
    }

    private async void Start()
    {
        _clickTexture = (Texture2D)await _cloudManager.GetTexture(_mainMenuController.TextureName);
        _clickImage.sprite = Sprite.Create(_clickTexture, new Rect(0, 0, _clickTexture.width, _clickTexture.height), Vector2.zero);
    }

    private async void OnDisable()
    {
        _clickButton.onClick.RemoveAllListeners();
        await _storageService.SaveAsync(_saveKey, _countClick);
    }

    public void ExitGame()
    {
        gameObject.SetActive(false);
        _mainMenuController.gameObject.SetActive(true);
    }

    private void OnClick()
    {
        _countClick++;
        OnCountClickChange?.Invoke(_countClick);
    }
}
