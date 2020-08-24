using UnityEngine;

public class PlayerArchetypeController : MonoBehaviour
{
    public Archetype archetype = null;
    public bool loadFromPrefs = false;

    private Animator _animator = null;
    private PlayerFSM _playerFSM = null;
    private CharacterGearController _gearController = null;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerFSM = GetComponent<PlayerFSM>();
        _gearController = GetComponent<CharacterGearController>();
    }

    private void Start()
    {
        if (loadFromPrefs == true)
        {
            LoadArchetypeFromPreset(Archetype.Load(Archetype.SAVE_PATH));
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

        if (archetype.attackEffects != null)
        {
            PlayerStateAttackEffects attackEffects = Instantiate(archetype.attackEffects, transform);

            _playerFSM.stateAttack.SetAttackEffects(attackEffects.effects);
        }

        if (CharacterGameEvent.instance != null)
        {
            PlayerData data = new PlayerData();

            data.Populate(archetype.configuration);

            CharacterGameEvent.instance.LoadingPlayer(data);
        }
    }
}