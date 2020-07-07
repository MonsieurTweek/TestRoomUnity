using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroupController : MonoBehaviour
{
    private List<TabButtonController> _tabButtons = null;

    [Header("References")]
    public TabButtonController selectedTab = null;

    [Header("Properties")]
    public Sprite tabIdle = null;
    public Sprite tabHover = null;
    public Sprite tabActive = null;

    public Color colorIdle = Color.white;
    public Color colorHover = Color.white;
    public Color colorActive = Color.white;

    private void Awake()
    {
        InputManager.instance.menu.Switch.performed += OnSwitchTab;
    }

    private void Start()
    {
        StartCoroutine(Initialize());
    }

    /// <summary>
    /// Inialize the tabs with the one to select by default
    /// </summary>
    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(0.1f);
        
        if (selectedTab != null)
        {
            OnTabSelected(selectedTab);
        }
    }

    /// <summary>
    /// Register a tab button to the list of tabs
    /// </summary>
    /// <param name="button">The button to register</param>
    public void Subscribe(TabButtonController button)
    {
        if (_tabButtons == null)
        {
            _tabButtons = new List<TabButtonController>();
        }

        _tabButtons.Add(button);
    }

    /// <summary>
    /// Hover on a particular tab button
    /// </summary>
    /// <param name="button">The button hover on</param>
    public void OnTabEnter(TabButtonController button)
    {
        ResetTabs();

        if (selectedTab == null || button != selectedTab)
        {
            // Apply color
            if (tabHover == null)
            {
                button.background.color = colorHover;
            }
            // Or sprite
            else
            {
                button.background.sprite = tabHover;
            }
        }
    }

    /// <summary>
    /// Leave the hover from a button
    /// </summary>
    /// <param name="button">The button we left hover</param>
    public void OnTabExit(TabButtonController button)
    {
        ResetTabs();
    }

    /// <summary>
    /// When pressing a button to select the tab
    /// </summary>
    /// <param name="button">The button to select</param>
    public void OnTabSelected(TabButtonController button)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = button;
        selectedTab.Select();

        ResetTabs();

        if (tabActive == null)
        {
            button.background.color = colorActive;
            button.title.color = colorIdle;
        }
        else
        {
            button.background.sprite = tabActive;
        }
    }

    /// <summary>
    /// Reset current tabs behavior to ensure no artifact
    /// </summary>
    public void ResetTabs()
    {
        foreach(TabButtonController button in _tabButtons)
        {
            if (selectedTab == null || button != selectedTab)
            {
                if (tabIdle == null)
                {
                    button.background.color = colorIdle;
                }
                else
                {
                    button.background.sprite = tabIdle;
                }
            }
        }
    }

    public virtual void SwapContent()
    {
        UnityEngine.Debug.LogWarning("Implement a custom swap content method");
    }

    private void OnSwitchTab(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        float axis = context.ReadValue<float>();

        if (axis < 0 && selectedTab.transform.GetSiblingIndex() > 0)
        {
            Transform sibling = transform.GetChild(selectedTab.transform.GetSiblingIndex() - 1);

            OnTabSelected(sibling.GetComponent<TabButtonController>());
        }

        if (axis > 0 && selectedTab.transform.GetSiblingIndex() < transform.childCount - 1)
        {
            Transform sibling = transform.GetChild(selectedTab.transform.GetSiblingIndex() + 1);

            OnTabSelected(sibling.GetComponent<TabButtonController>());
        }
    }

    private void OnDestroy()
    {
        if (InputManager.instance != null)
        {
            InputManager.instance.menu.Switch.performed -= OnSwitchTab;
        }
    }
}