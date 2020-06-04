using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WeaponController : GearController
{
    public int damage = 0;
    public int heavyModifier = 1;

    protected BoxCollider _collider = null;
    private int _currentModifier = 0;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CanHit(other) == true)
        {
            _currentModifier = ((CharacterStateAttack)owner.currentState).isHeavy == true ? heavyModifier : 1;
            Debug.Log(gameObject.name);
            Hit(other.GetComponent<ICharacter>());
        }
    }

    public virtual void Hit(ICharacter character)
    {
        if (character != null)
        {
            character.Hit(damage * _currentModifier);
        }
    }

    protected virtual bool CanHit(Collider other)
    {
        return owner.currentState.flag == StateEnum.ATTACK && 
            other.tag != owner.tag && 
            (other.tag == EnemyData.TAG || other.tag == PlayerData.TAG);
    }
}