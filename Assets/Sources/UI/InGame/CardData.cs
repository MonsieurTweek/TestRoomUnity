using UnityEngine;

public class CardData : AbstractUniqueData
{
    public string title { get { return descriptiveObject.title; } }
    public string description { get { return descriptiveObject.description; } }
    public Sprite icon { get { return descriptiveObject.icon; } }

    public DescriptiveObject descriptiveObject { private set; get; }

    public void Populate(DescriptiveObject descriptiveObject)
    {
        this.descriptiveObject = descriptiveObject;
    }
}