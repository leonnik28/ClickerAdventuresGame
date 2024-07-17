using System;
using UnityEngine;
using UnityEngine.UI;

public class ClickerSession : MonoBehaviour
{
    public static Action<int> OnCountClickChange;

    [SerializeField] private Button _clickButton;

    private int _countClick;

    private void OnEnable()
    {
        _clickButton.onClick.AddListener(OnClick);
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
