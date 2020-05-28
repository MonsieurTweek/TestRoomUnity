using UnityEngine;

public class PlayerArchetypeController : MonoBehaviour
{
    public Archetype archetype = null;
    public bool loadFromPrefs = false;

    private Animator _animator = null;
    private CharacterGearController _gearController = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _gearController = GetComponent<CharacterGearController>();

    }
    private void Start()
    {
        if (loadFromPrefs == true)
        {
        }
        else if (archetype != null)
        {
            LoadArchetypeFromPreset(archetype);
        }
    }

    private void LoadArchetypeFromPreset(Archetype archetype)
    {
        _animator.runtimeAnimatorController = archetype.animationController;

        if (archetype.gearLeft != null)
        {
            _gearController.InstantiateGear(archetype.gearLeft, _gearController.anchorLeft);
        }

        if (archetype.gearRight != null)
        {
            _gearController.InstantiateGear(archetype.gearRight, _gearController.anchorRight);
        }
    }
}