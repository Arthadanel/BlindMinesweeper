using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Text scoreText;
    [SerializeField] private MineField mineField;

    public static bool GameInputEnabled { get; private set; }
    private const string RestartText = "РЕСТАРТ";
    private int _score;
    private int Score
    {
        get => _score;
        set
        {
            _score = value;
            scoreText.text = Score.ToString();
        }
    }

    private Color _correctColor;
    
    void Start()
    {
        SwitchInputMode(true);
        mineField.DetonateCell = DetonateCell;
        startButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        StopAllCoroutines();
        startButton.GetComponentInChildren<Text>().text = RestartText;
        Score = 0;
        StartCoroutine(DisplayField());
    }

    private IEnumerator DisplayField()
    {
        mineField.GenerateField();
        yield return new WaitForSeconds(3);
        mineField.ChangeMinesVisibility(true);
        SwitchInputMode(false);
    }

    private void DetonateCell(Color color)
    {
        if (Score == 0)
            _correctColor = color;
        if (_correctColor == color)
            Score++;
        else
            EndGame();
    }

    private void EndGame()
    {
        SwitchInputMode(true);
    }

    private static void SwitchInputMode(bool onlyUI)
    {
        GameInputEnabled = !onlyUI;
    }
}
