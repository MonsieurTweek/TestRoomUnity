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

    private GearController _gearLeftAttached = null;
    private GearController _gearRightAttached = null;

    private void Awake()
    {
        if (gearLeft != null)
        {
            _gearLeftAttached = InstantiateGear(gearLeft, anchorLeft);
        }

        if (gearRight != null)
        {
            _gearRightAttached = InstantiateGear(gearRight, anchorRight);
        }
    }

    private void Start()
    {
        CharacterGameEvent.instance.onDying += OnDying;
    }

    public GearController InstantiateGear(GearController prefab, Transform anchor)
    {
        UnityEngine.Assertions.Assert.IsTrue(anchor != null, "Missing anchor to attach " + prefab.name);

        GearController gear = Instantiate(prefab, anchor);
        float scaleModifier = gear.transform.localScale.x / gear.transform.lossyScale.x;

        gear.transform.localScale = gear.transform.localScale * scaleModifier;
        gear.Attach(owner);

        return gear;
    }

    private void OnDying(uint uniqueId)
    {
        if (owner.data.uniqueId == uniqueId)
        {
            if (_gearLeftAttached != null)
            {
                Destroy(_gearLeftAttached.gameObject);
            }

            if (_gearRightAttached != null)
            {
                Destroy(_gearRightAttached.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onDying -= OnDying;
        }
    }
}
