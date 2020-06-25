using System.Collections.Generic;
using UnityEngine;

public class CardCanvasController : MonoBehaviour
{
    public GameObject root = null;
    public List<CardController> cards = new List<CardController>();

    public List<Perk> perks = new List<Perk>();

    private HashSet<int> perksToDisplay = new HashSet<int>();

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

        perksToDisplay.Clear();

        int index = 0;

        for (int i = 0; i < cards.Count; ++i)
        {
            do
            {
                index = Random.Range(0, perks.Count);
            }
            while (perksToDisplay.Contains(index) && perks.Count > 1);

            perksToDisplay.Add(index);

            cards[i].Initialize(perks[index]);
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

                if (perk.isCumulative == false)
                {
                    perks.Remove(perk);
                }
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

                PerkGameEvent.instance.Select(cards[i].data);
            }
        }
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