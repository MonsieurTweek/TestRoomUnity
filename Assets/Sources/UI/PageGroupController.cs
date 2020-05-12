using System.Collections.Generic;
using UnityEngine;

public class PageGroupController : TabGroupController
{
    public List<GameObject> pages = null;

    public override void SwapContent()
    {
        int index = selectedTab.transform.GetSiblingIndex();

        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].SetActive(true);

            if (i != index)
            {
                pages[i].SetActive(false);
            }
        }
    }
}
