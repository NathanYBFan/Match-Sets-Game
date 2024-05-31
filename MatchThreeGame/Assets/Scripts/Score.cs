using TMPro;
using UnityEngine;

public sealed class Score : MonoBehaviour
{
    public static Score _Instance { get; private set; }

    private int _score;

    public int totalScore { 
        get => _score;
        set
        {
            if (_score == value) return;
            _score = value;

            scoreTextBox.SetText($"Score: {_score}");
        }
    }

    [SerializeField]
    private TextMeshProUGUI scoreTextBox;


    private void Awake() => _Instance = this;
}
