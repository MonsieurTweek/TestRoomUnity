using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardCanvasController : MonoBehaviour
{
    [Header("References")]
    public GameObject root = null;
    public List<CardController> cards = new List<CardController>();

    [Header("Properties")]
    public List<Perk> perks = new List<Perk>();

    private HashSet<int> _perksToDisplay = new HashSet<int>();
    private int _currentCardIndex = 0;
    private int _currentIntroCounter = 0;

    private void Awake()
    {
        root.SetActive(false);
    }

    private void Start()
    {
        PerkGameEvent.instance.onDisplayed += OnPerkDisplayed;
        PerkGameEvent.instance.onUnlocked += OnPerkUnlocked;
    }

    private void OnNavigatePerformed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        int index = _currentCardIndex;

        if (input.x < 0f)
        {
            index--;
        }
        else if (input.x > 0f)
        {
            index++;
        }

        if (index >= 0 && index < cards.Count)
        {
            cards[_currentCardIndex].Deselect();

            _currentCardIndex = index;

            cards[_currentCardIndex].Select();
        }
    }

    private void OnConfirmStarted(InputAction.CallbackContext context)
    {
        cards[_currentCardIndex].ConfirmSelection();
    }

    private void OnPerkDisplayed()
    {
        root.SetActive(true);

        _currentIntroCounter = 0;
        _perksToDisplay.Clear();

        int index = 0;

        for (int i = 0; i < cards.Count; ++i)
        {
            do
            {
                index = Random.Range(0, perks.Count);
            }
            while (_perksToDisplay.Contains(index) && perks.Count > 1);

            _perksToDisplay.Add(index);

            cards[i].Initialize(perks[index]);
            cards[i].AnimateIntro(OnFullIntroCompleted);
        }
    }

    private void OnFullIntroCompleted()
    {
        _currentIntroCounter++;

        if (_currentIntroCounter >= cards.Count)
        {
            cards[_currentCardIndex].Select();
        }

        InputManager.instance.menu.Navigate.performed += OnNavigatePerformed;
        InputManager.instance.menu.Confirm.started += OnConfirmStarted;
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

        InputManager.instance.menu.Navigate.performed -= OnNavigatePerformed;
        InputManager.instance.menu.Confirm.started -= OnConfirmStarted;
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