using UnityEngine;

public class WeaponController : GearController
{
    public int damage = 0;
    public int modifier = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (CanHit(other) == true)
        {
            modifier = ((CharacterStateAttack)owner.currentState).isHeavy == true ? 2 : 1;

            Hit(other.GetComponent<ICharacter>());
        }
    }

    public virtual void Hit(ICharacter character)
    {
        if (character != null)
        {
            character.Hit(damage * modifier);
        }
    }

    private bool CanHit(Collider other)
    {
        return owner.currentState.flag == StateEnum.ATTACK && 
            other.tag != owner.tag && 
            (other.tag == EnemyData.TAG || other.tag == PlayerData.TAG);
    }
}