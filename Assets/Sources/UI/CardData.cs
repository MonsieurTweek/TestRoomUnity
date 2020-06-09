using UnityEngine;

public class CardData : AbstractUniqueData
{
    public string title = string.Empty;
    public string description = string.Empty;
    public Sprite icon = null;

    public void Populate(DescriptiveObject descriptiveObject)
    {
        title = descriptiveObject.title;
        description = descriptiveObject.description;
        icon = descriptiveObject.icon;
    }
}