﻿using UnityEngine;

public class CharacterGearController : MonoBehaviour
{
    public CharacterFSM owner = null;

    public bool enableOnAwake = true;

    [Header("Anchor")]
    public Transform anchorLeft = null;
    public Transform anchorRight = null;

    [Header("Gear")]
    public GearController gearLeft = null;
    public GearController gearRight = null;

    private GearController _gearLeftAttached = null;
    private GearController _gearRightAttached = null;

    private bool _isGearActive = true;

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

        if (enableOnAwake == false)
        {
            WeaponSwitch();
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

        // Scale the gear to the character
        if (gear.preserveScale == false)
        {
            float scaleModifier = gear.transform.localScale.x / gear.transform.lossyScale.x;

            gear.transform.localScale = gear.transform.localScale * scaleModifier;
        }

        gear.Attach(owner);

        return gear;
    }

    public void WeaponSwitch()
    {
        // Sheath or unsheath gearrs
        _isGearActive = !_isGearActive;

        if (gearLeft != null)
        {
            _gearLeftAttached.gameObject.SetActive(_isGearActive);
        }

        if (gearRight != null)
        {
            _gearRightAttached.gameObject.SetActive(_isGearActive);
        }
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
