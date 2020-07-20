using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WeaponController : GearController
{
    public int damage = 0;

    protected BoxCollider _collider = null;

    protected override void Awake()
    {
        base.Awake();

        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CanHit(other) == true)
        {
            Hit(other.GetComponent<ICharacter>());
        }
    }

    protected virtual bool CanHit(Collider other)
    {
        return owner.currentState.flag == (uint)CharacterStateEnum.ATTACK &&
            ((CharacterStateAttack)owner.currentState).isEnabled == true &&
            other.tag != owner.tag && 
            (other.tag == EnemyData.TAG || other.tag == PlayerData.TAG);
    }

    public virtual void Hit(ICharacter character, bool isMelee = true)
    {
        if (character != null && character.Hit(damage) == true)
        {
            CharacterFSM target = (CharacterFSM)character;

            CharacterGameEvent.instance.Hitting(owner.data, target.data);
        }
    }
}