using UnityEngine;
using Zenject;

public class MainMenuController : MonoBehaviour
{
    public Texture2D ClickerTexture => _clickerTexture;

    [SerializeField] private GameObject _clicker;
    [SerializeField] private GameObject _walker;
    [SerializeField] private string _imageKey = "ClickHere";

    private Texture2D _clickerTexture;

    private CloudManager _loadManager;

    [Inject]
    private void Construct(CloudManager loadManager)
    {
        _loadManager = loadManager;
    }

    public void StartClicker()
    {
        LoadTexture();
        _clicker.SetActive(true);
        _walker.SetActive(false);
        gameObject.SetActive(false);
    }

    public void StartWalker()
    {
        _walker.SetActive(true);
        _clicker.SetActive(false);
        gameObject.SetActive(false);
    }

    public async void LoadTexture()
    {
        _clickerTexture = (Texture2D)await _loadManager.GetTexture(_imageKey);
    }
}
