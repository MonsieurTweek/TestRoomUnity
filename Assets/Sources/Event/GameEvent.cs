using System;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static GameEvent instance { private set; get; }

    private void Awake()
    {
        // First destroy any existing instance of it
        if (instance != null)
        {
            Destroy(instance);
        }

        // Then reassign a proper one
        instance = this;
    }

    public event Action onPlayButtonPressed;
    public void PlayButtonPressed()
    {
        if (instance.onPlayButtonPressed != null)
        {
            instance.onPlayButtonPressed();
        }
    }

    public event Action onStoreButtonPressed;
    public void StoreButtonPressed()
    {
        if (instance.onStoreButtonPressed != null)
        {
            instance.onStoreButtonPressed();
        }
    }

    public event Action onHomeButtonPressed;
    public void HomeButtonPressed()
    {
        if (instance.onHomeButtonPressed != null)
        {
            instance.onHomeButtonPressed();
        }
    }

    public event Action onCharacterSelected;
    public void CharacterSelected()
    {
        if (instance.onCharacterSelected != null)
        {
            instance.onCharacterSelected();
        }
    }

    public event Action<bool> onGameOver;
    public void GameOverRaised(bool hasWon)
    {
        if (instance.onGameOver != null)
        {
            instance.onGameOver(hasWon);
        }
    }

    public event Action onLoadingEnded;
    public void LoadingEnded()
    {
        if (instance.onLoadingEnded != null)
        {
            instance.onLoadingEnded();
        }
    }
}