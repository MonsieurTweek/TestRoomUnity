using System.Collections.Generic;
using UnityEngine;

public class CardCanvasController : MonoBehaviour
{
    public GameObject root = null;
    public List<CardController> cards = new List<CardController>();

    private void Awake()
    {
        root.SetActive(false);
    }

    private void Start()
    {
        PerkGameEvent.instance.onDisplayed += OnPerkDisplayed;
        PerkGameEvent.instance.onUnlocked += OnPerkUnlocked;
    }

    private void OnPerkDisplayed()
    {
        root.SetActive(true);

        for (int i = 0; i < cards.Count; ++i)
        {
            cards[i].AnimateIntro();
        }
    }

    private void OnPerkUnlocked(uint id, Perk perk)
    {
        for (int i = 0; i < cards.Count; ++i)
        {
            if (cards[i].data.uniqueId == id)
            {
                cards[i].AnimateSelection(OnSelectionEnded);
            }
            else
            {
                cards[i].AnimateHide();
            }
        }

        root.SetActive(false);
    }

    private void OnSelectionEnded(object id)
    {
        for (int i = 0; i < cards.Count; ++i)
        {
            if (cards[i].data.uniqueId == (uint)id)
            {
                cards[i].Hide();
            }
        }

        CharacterGameEvent.instance.Pause(false);
    }

    private void OnDestroy()
    {
        if (PerkGameEvent.instance != null)
        {
            PerkGameEvent.instance.onDisplayed -= OnPerkDisplayed;
            PerkGameEvent.instance.onUnlocked -= OnPerkUnlocked;
        }
    }
}