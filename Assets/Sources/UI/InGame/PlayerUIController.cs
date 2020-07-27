﻿using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [Header("References")]
    public PlayerFSM player = null;
    public GameObject layout = null;
    public ResourceGaugeController playerEnergy = null;
    public ResourceGaugeController playerHealth = null;
    public ResourceGaugeController targetHealth = null;
    public PerkController[] perks = null;

    private void Awake()
    {
        for (int i = 0; i < perks.Length; i++)
        {
            perks[i].Initialize(i);
        }

        CharacterGameEvent.instance.onPlayerLoaded += OnPlayerLoaded;
    }

    private void OnPlayerLoaded()
    {
        CharacterGameEvent.instance.onHit += OnCharacterHit;
        CharacterGameEvent.instance.onEnergyUpdated += OnEnergyUpdated;
        CharacterGameEvent.instance.onOutOfEnergy += OnOutOfEnergy;
        CharacterGameEvent.instance.onTargetSelected += OnTargetSelected;
        CharacterGameEvent.instance.onTargetDeselected += OnTargetDeselected;

        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;

        player.data.onBuffValues += RefreshPlayerData;

        RefreshPlayerData();

        playerEnergy.Refresh(100, 100);
    }

    private void OnIntroStarted(Transform target, AbstractCharacterData data)
    {
        layout.SetActive(false);
    }

    private void OnIntroEnded()
    {
        layout.SetActive(true);
    }

    private void RefreshPlayerData()
    {
        playerHealth.Refresh(player.data.health, player.data.healthMax);
    }

    private void OnEnergyUpdated(uint characterUniqueId, float energy)
    {
        if (player.data.uniqueId == characterUniqueId)
        {
            int current = Mathf.RoundToInt(energy / ((PlayerData)player.data).energyMax * 100f);

            playerEnergy.Refresh(current, 100);
        }
    }

    private void OnOutOfEnergy(uint characterUniqueId)
    {
        if (player.data.uniqueId == characterUniqueId)
        {
            if (LeanTween.isTweening(playerEnergy.gameObject) == false)
            {
                LeanTween.scale(playerEnergy.gameObject, playerEnergy.transform.localScale * 1.2f, 0.25f)
                    .setEase(LeanTweenType.easeShake)
                    .setLoopPingPong(1);
            }
        }
    }

    private void OnCharacterHit(uint id, int health, int damage)
    {
        if (player.data.uniqueId == id)
        {
            playerHealth.Refresh(health, player.data.healthMax);
        }
        else if (player.target != null && player.target.data.uniqueId == id)
        {
            targetHealth.Refresh(health, player.target.data.healthMax);
        }
    }

    private void OnTargetSelected(uint id, int health, int healthMax)
    {
        targetHealth.Toggle(true);

        targetHealth.Refresh(health, healthMax);
    }

    private void OnTargetDeselected(uint id)
    {
        targetHealth.Toggle(false);
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onPlayerLoaded -= OnPlayerLoaded;

            CharacterGameEvent.instance.onHit -= OnCharacterHit;
            CharacterGameEvent.instance.onEnergyUpdated -= OnEnergyUpdated;
            CharacterGameEvent.instance.onOutOfEnergy -= OnOutOfEnergy;
            CharacterGameEvent.instance.onTargetSelected -= OnTargetSelected;
            CharacterGameEvent.instance.onTargetDeselected -= OnTargetDeselected;

            CharacterGameEvent.instance.onIntroStarted -= OnIntroStarted;
            CharacterGameEvent.instance.onIntroEnded -= OnIntroEnded;
        }

        if (player != null && player.data != null)
        {
            player.data.onBuffValues -= RefreshPlayerData;
        }
    }
}