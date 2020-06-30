using UnityEngine;

public class PlayerOutlineController : CharacterOutlineController
{
    public PlayerCustomizationController customizationController = null;

    public override void Show()
    {
        if (_meshRenderers.Count == 0)
        {
            GetSkinnedMeshRenderers();
        }

        base.Show();
    }

    private void GetSkinnedMeshRenderers()
    {
        foreach (CustomizationPartOnPlayer part in customizationController.customizableParts)
        {
            foreach (GameObject element in part.elements)
            {
                if (element.activeSelf == true)
                {
                    _meshRenderers.Add(element.GetComponent<SkinnedMeshRenderer>());
                }
            }
        }
    }
}