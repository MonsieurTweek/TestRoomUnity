using System.Collections.Generic;
using UnityEngine;

public class MaterialGroupController : TabGroupController
{
    public List<Material> materials = null;

    public override void SwapContent()
    {
        int index = selectedTab.transform.GetSiblingIndex();

        if (CustomizationPartController.currentPart != null)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                if (i == index)
                {
                    CustomizationPartController.currentPart.SetMaterial(materials[i]);
                }
            }
        }
    }
}
