using UnityEngine;

public class WeaponController : GearController
{
    public int damage = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision from " + this.name + " on " + other.name + " with state " + owner.currentState);

        if (owner.currentState.flag == StateEnum.ATTACK && other.tag != owner.tag && (other.tag == EnemyData.TAG || other.tag == PlayerData.TAG))
        {
            Hit(other.GetComponent<ICharacter>());
        }
    }

    public virtual void Hit(ICharacter character)
    {
        if (character != null)
        {
            character.Hit(damage);
        }
    }
}