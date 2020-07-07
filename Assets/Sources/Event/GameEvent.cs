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

    public event Action<bool, int> onGameOver;
    public void GameOver(bool hasWon, int reward)
    {
        if (instance.onGameOver != null)
        {
            instance.onGameOver(hasWon, reward);
        }
    }

    public event Action onDataSaved;
    public void DataSaved()
    {
        if (instance.onDataSaved != null)
        {
            instance.onDataSaved();
        }
    }

    public event Action<bool> onControlSwitched;
    public void ControlSwitched(bool isKeyboard)
    {
        if (instance.onControlSwitched != null)
        {
            instance.onControlSwitched(isKeyboard);
        }
    }
}