using TMPro;
using UnityEngine;

public class GameOverCanvasController : MonoBehaviour
{
    public CanvasGroup root = null;
    public TextMeshProUGUI title = null;

    public Color victoryColor = Color.black;
    public string victoryText = string.Empty;

    public Color defeatColor = Color.black;
    public string defeatText = string.Empty;

    private void Start()
    {
        GameStateGameOver state = (GameStateGameOver)GameManager.instance.GetCurrentState();

        if (state != null)
        {
            title.text = state.hasWon ? victoryText : defeatText;
            title.color = state.hasWon ? victoryColor : defeatColor;
        }

        root.alpha = 0f;

        LeanTween.alphaCanvas(root, 1f, 1f);
    }
}