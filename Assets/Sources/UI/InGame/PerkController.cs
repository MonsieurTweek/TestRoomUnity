using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PerkController : MonoBehaviour
{
    private static int CURRENT_INDEX = 0;

    [Header("References")]
    public Image icon = null;

    private int _index = 0;

    public void Initialize(int index)
    {
        _index = index;
    }

    private void Start()
    {
        PerkGameEvent.instance.onUnlocked += OnPerkUnlocked;
    }

    private void OnPerkUnlocked(uint uniqueId, Perk perk)
    {
        if (CURRENT_INDEX == _index)
        {
            icon.sprite = perk.icon;
            icon.enabled = true;

            StartCoroutine(IncreaseIndex());
        }
    }

    private IEnumerator IncreaseIndex()
    {
        yield return new WaitForEndOfFrame();

        CURRENT_INDEX++;
    }

    private void OnDestroy()
    {
        if (PerkGameEvent.instance != null)
        {
            PerkGameEvent.instance.onUnlocked -= OnPerkUnlocked;
        }

        CURRENT_INDEX = 0;
    }
}