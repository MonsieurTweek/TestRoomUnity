using System.Collections.Generic;
using UnityEngine;

public class AnalyticsObserver : MonoBehaviour
{
    private int _currentLevelIndex = 1;

    private void Start()
    {
        GameEvent.instance.onStateChanged += OnStateChanged;
        GameEvent.instance.onGameOver += OnGameOver;
        GameEvent.instance.onControlSwitched += OnControlSwitched;

        LoadingGameEvent.instance.onPlayerLoading += OnPlayerLoading;
        LoadingGameEvent.instance.onPlayerLoaded += OnPlayerLoaded;

        PerkGameEvent.instance.onDisplayEnded += OnPerkDisplayEnded;
        PerkGameEvent.instance.onUnlockEnded += OnPerkUnlockEnded;
    }

    private void OnStateChanged(GameStateEnum fromState, GameStateEnum toState)
    {
        AnalyticsManager.ChangeState(fromState, toState);

        switch(toState)
        {
            case GameStateEnum.HOME:
                if (fromState == GameStateEnum.TITLE)
                    AnalyticsManager.FirstInteraction();
            break;

            case GameStateEnum.STORE:
                AnalyticsManager.StoreOpened();
            break;
        }
    }

    private void OnPerkDisplayEnded(List<Perk> perks)
    {
        AnalyticsManager.DisplayPerks(perks);
    }

    private void OnPerkUnlockEnded(uint uniqueId, Perk perk)
    {
        AnalyticsManager.UnlockPerk(perk);
    }

    private void OnPlayerLoading(PlayerData playerData)
    {
        AnalyticsManager.SpawnPlayer(playerData.name);
    }

    private void OnPlayerLoaded()
    {
        AnalyticsManager.GameStart();

        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;
    }

    private void OnIntroStarted(Transform _, AbstractCharacterData __)
    {
        AnalyticsManager.LevelComplete(_currentLevelIndex);
    }

    private void OnIntroEnded()
    {
        AnalyticsManager.LevelStart(_currentLevelIndex++);
    }

    private void OnControlSwitched(bool isKeyboard)
    {
        AnalyticsManager.SwitchControl(isKeyboard);
    }

    private void OnGameOver(bool hasWon, int rewards)
    {
        if (hasWon == true)
        {
            AnalyticsManager.LevelComplete(_currentLevelIndex);
        }
        else
        {
            AnalyticsManager.LevelFail(_currentLevelIndex);
        }

        AnalyticsManager.GameOver(hasWon);
    }
}