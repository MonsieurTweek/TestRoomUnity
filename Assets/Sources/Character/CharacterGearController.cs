using UnityEngine;

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

    public GearController gearLeftAttached { private set; get; }
    public GearController gearRightAttached { private set; get; }

    private bool _isGearActive = true;

    private void Awake()
    {
        Initialize(gearLeft, gearRight);

        if (enableOnAwake == false)
        {
            ToggleGear();
        }
    }

    private void Start()
    {
        CharacterGameEvent.instance.onDying += OnDying;
    }

    public void Initialize(GearController gearLeft, GearController gearRight)
    {
        if (gearLeft != null)
        {
            gearLeftAttached = InstantiateGear(gearLeft, anchorLeft);
        }

        if (gearRight != null)
        {
            gearRightAttached = InstantiateGear(gearRight, anchorRight);
        }
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

    public void ToggleGear()
    {
        // Sheath or unsheath gears
        _isGearActive = !_isGearActive;

        if (gearLeft != null)
        {
            gearLeftAttached.gameObject.SetActive(_isGearActive);
        }

        if (gearRight != null)
        {
            gearRightAttached.gameObject.SetActive(_isGearActive);
        }
    }

    public void PlayFx(int gearIndex)
    {
        switch(gearIndex)
        {
            case 0:
                if (gearRightAttached != null)
                    gearRightAttached.PlayFx();
            break;
            case 1:
                if (gearLeftAttached != null)
                    gearLeftAttached.PlayFx();
            break;
            case 2:
                if (gearRightAttached != null)
                    gearRightAttached.PlayFx();
                if (gearLeftAttached != null)
                    gearLeftAttached.PlayFx();
            break;
        }
    }

    public void PlayAttackFx()
    {
        WeaponController weaponController = (WeaponController)gearRightAttached;

        if (weaponController != null && weaponController.attackFx != null)
        {
            weaponController.attackFx.Reset((uint)CharacterStateEnum.ATTACK);
        }
    }

    public void StopAttackFx()
    {
        WeaponController weaponController = (WeaponController)gearRightAttached;

        if (weaponController != null && weaponController.attackFx != null)
        {
            weaponController.attackFx.Stop();
        }
    }

    public void PlayComboFx()
    {
        WeaponController weaponController = (WeaponController)gearRightAttached;

        if (weaponController != null && weaponController.comboFx != null && weaponController.attackFx != null)
        {
            weaponController.comboFx.Reset((uint)CharacterStateEnum.ATTACK);
            weaponController.attackFx.Stop();
        }
    }

    public void StopComboFx()
    {
        WeaponController weaponController = (WeaponController)gearRightAttached;

        if (weaponController != null && weaponController.comboFx != null)
        {
            weaponController.comboFx.Stop();
        }
    }

    private void OnDying(uint uniqueId, CharacterTypeEnum type)
    {
        if (owner.data.uniqueId == uniqueId)
        {
            if (gearLeftAttached != null)
            {
                Destroy(gearLeftAttached.gameObject);
            }

            if (gearRightAttached != null)
            {
                Destroy(gearRightAttached.gameObject);
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
