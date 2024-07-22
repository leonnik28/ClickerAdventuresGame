using UnityEngine;
using Zenject;

public class MainMenuController : MonoBehaviour
{
    public string TextureName = "ClickHere";

    [SerializeField] private GameObject _clicker;
    [SerializeField] private GameObject _walker;

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
        await _loadManager.GetTexture(TextureName);
    }
}
