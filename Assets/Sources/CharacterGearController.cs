using UnityEngine;

public class CharacterGearController : MonoBehaviour
{
    public CharacterFSM owner = null;

    [Header("Anchor")]
    public Transform anchorLeft = null;
    public Transform anchorRight = null;

    [Header("Gear")]
    public GearController gearLeft = null;
    public GearController gearRight = null;

    private void Awake()
    {
        if (gearLeft != null)
        {
            InstantiateGear(gearLeft, anchorLeft);
        }

        if (gearRight != null)
        {
            InstantiateGear(gearRight, anchorRight);
        }
    }

    public void InstantiateGear(GearController prefab, Transform anchor)
    {
        UnityEngine.Assertions.Assert.IsTrue(anchor != null, "Missing anchor to attach " + prefab.name);

        GearController gear = Instantiate(prefab, anchor);
        float scaleModifier = gear.transform.localScale.x / gear.transform.lossyScale.x;

        gear.transform.localScale = gear.transform.localScale * scaleModifier;
        gear.Attach(owner);
    }
}
