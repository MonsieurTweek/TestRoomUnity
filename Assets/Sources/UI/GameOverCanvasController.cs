using TMPro;
using UnityEngine;

public class GameOverCanvasController : MonoBehaviour
{
    public TextMeshProUGUI title = null;

    public Color victoryColor = Color.black;
    public string victoryText = string.Empty;

    public Color defeatColor = Color.black;
    public string defeatText = string.Empty;

    private void Start()
    {
        GameEvent.instance.onGameOver += OnGameOver;
    }

    private void OnGameOver(bool hasWon)
    {
        title.text = hasWon ? victoryText : defeatText;
        title.color = hasWon ? victoryColor : defeatColor;
    }

    private void OnDestroy()
    {
        if (GameEvent.instance != null)
        {
            GameEvent.instance.onGameOver -= OnGameOver;
        }
    }
}
