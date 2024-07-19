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

    private StorageService _storageService;

    [Inject]
    private void Construct(StorageService storageService)
    {
        _storageService = storageService;
    }

    private void OnEnable()
    {
        _clickButton.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        _clickImage.sprite = Sprite.Create(_mainMenuController.ClickerTexture, new Rect(0, 0, _mainMenuController.ClickerTexture.width, _mainMenuController.ClickerTexture.height), Vector2.zero);
    }

    private void OnDisable()
    {
        _clickButton.onClick.RemoveAllListeners();
    }

    private void OnClick()
    {
        _countClick++;
        OnCountClickChange?.Invoke(_countClick);
    }
}
