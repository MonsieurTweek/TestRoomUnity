using System.Collections.Generic;

public class AnimatedPageGroupController : TabGroupController
{
    public float timeToShow = 0.5f;
    public float timeToHide = 0.5f;

    public List<AnimatedPageController> pages = null;

    public override void SwapContent()
    {
        int index = selectedTab.transform.GetSiblingIndex();

        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].Show(timeToShow);

            if (i != index)
            {
                pages[i].Hide(timeToHide);
            }
        }
    }
}
