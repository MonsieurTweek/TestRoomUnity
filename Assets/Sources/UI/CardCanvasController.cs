using System.Collections.Generic;
using UnityEngine;

public class CardCanvasController : MonoBehaviour
{
    public List<CardController> cards = new List<CardController>();

    private void Start()
    {
        PerkGameEvent.instance.onDisplay += OnPerkDisplayed;
        PerkGameEvent.instance.onPerkSelected += OnPerkSelected;
    }

    private void OnPerkDisplayed()
    {
        for (int i = 0; i < cards.Count; ++i)
        {
            cards[i].AnimateIntro();
        }
    }

    private void OnPerkSelected(uint id)
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

        CharacterGameEvent.instance.PauseRaised(false);
    }

    private void OnDestroy()
    {
        if (PerkGameEvent.instance != null)
        {
            PerkGameEvent.instance.onDisplay -= OnPerkDisplayed;
            PerkGameEvent.instance.onPerkSelected -= OnPerkSelected;
        }
    }
}