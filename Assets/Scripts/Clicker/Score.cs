using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _scoreText;

    private void OnEnable()
    {
        ClickerSession.OnCountClickChange += ChangeScore;
    }

    private void OnDisable()
    {
        ClickerSession.OnCountClickChange -= ChangeScore;
    }

    private void ChangeScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
