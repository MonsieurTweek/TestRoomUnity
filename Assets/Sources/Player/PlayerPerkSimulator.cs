using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPerkSimulator : MonoBehaviour
{
    public PlayerFSM player = null;
    public List<Perk> perks = new List<Perk>();

#if UNITY_EDITOR
    private void Awake()
    {
        CharacterGameEvent.instance.onPlayerLoaded += OnPlayerLoaded;
    }

    private void OnPlayerLoaded()
    {
        StartCoroutine(UnlockPerk());
    }

    private IEnumerator UnlockPerk()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < perks.Count; i++)
        {
            CardData data = new CardData();
            data.Populate(perks[i]);

            Debug.Log("Unlock perk " + data.title + " for simulation");

            PerkGameEvent.instance.StartUnlock(data);
        }
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onPlayerLoaded -= OnPlayerLoaded;
        }
    }
#endif
}
