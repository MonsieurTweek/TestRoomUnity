using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvent : MonoBehaviour
{
    private static GameEvent _instance = null;
    public static GameEvent instance { get { return _instance; } set { _instance = value; } }

    private void Awake()
    {
        _instance = this;
    }

    public event Action onPlayButtonPressed;
    public void PlayButtonPressed()
    {
        if (onPlayButtonPressed != null)
        {
            onPlayButtonPressed();
        }
    }

    public event Action onStoreButtonPressed;
    public void StoreButtonPressed()
    {
        if (onStoreButtonPressed != null)
        {
            onStoreButtonPressed();
        }
    }

    public event Action onHomeButtonPressed;
    public void HomeButtonPressed()
    {
        if (onHomeButtonPressed != null)
        {
            onHomeButtonPressed();
        }
    }

    public event Action onCharacterSelected;
    public void CharacterSelected()
    {
        if (onCharacterSelected != null)
        {
            onCharacterSelected();
        }
    }

    public event Action<bool> onGameOver;
    public void GameOverRaised(bool hasWon)
    {
        if (onGameOver != null)
        {
            onGameOver(hasWon);
        }
    }
}