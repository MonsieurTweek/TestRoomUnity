using System.Collections.Generic;
using UnityEngine;

public class TabGroupController : MonoBehaviour
{
    public List<TabButtonController> tabButtons = null;
    public List<GameObject> pages = null;

    public Sprite tabIdle = null;
    public Sprite tabHover = null;
    public Sprite tabActive = null;

    public TabButtonController selectedTab = null;

    private void Awake()
    {
        if (selectedTab != null)
        {
            OnTabSelected(selectedTab);
        }
    }

    public void Subscribe(TabButtonController button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButtonController>();
        }

        tabButtons.Add(button);
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
        foreach(TabButtonController button in tabButtons)
        {
            if (selectedTab == null || button != selectedTab)
            {
                button.background.sprite = tabIdle;
            }
        }
    }

    public void SwapContent()
    {
        int index = selectedTab.transform.GetSiblingIndex();

        for (int i = 0; i < pages.Count; i++)
        {
            if (i == index)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }
}