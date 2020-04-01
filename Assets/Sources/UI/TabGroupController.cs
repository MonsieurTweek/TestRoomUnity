using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroupController : MonoBehaviour
{
    private List<TabButtonController> _tabButtons = null;

    public Sprite tabIdle = null;
    public Sprite tabHover = null;
    public Sprite tabActive = null;

    public TabButtonController selectedTab = null;

    private void Start()
    {
        StartCoroutine("Initialize");
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForEndOfFrame();
        
        if (selectedTab != null)
        {
            OnTabSelected(selectedTab);
        }
    }

    public void Subscribe(TabButtonController button)
    {
        if (_tabButtons == null)
        {
            _tabButtons = new List<TabButtonController>();
        }

        _tabButtons.Add(button);
    }

    public void OnTabEnter(TabButtonController button)
    {
        ResetTabs();

        if (selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButtonController button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButtonController button)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = button;
        selectedTab.Select();

        ResetTabs();

        button.background.sprite = tabActive;
    }

    public void ResetTabs()
    {
        foreach(TabButtonController button in _tabButtons)
        {
            if (selectedTab == null || button != selectedTab)
            {
                button.background.sprite = tabIdle;
            }
        }
    }

    public virtual void SwapContent()
    {
        UnityEngine.Debug.LogWarning("Implement a custom swap content method");
    }
}