using UnityEngine;

public class ProjectileController : WeaponController
{
    public float speed = 0f;
    public GameObject impactFx = null;
    public bool destroyAtImpact = true;
    private Vector3 _direction = Vector3.zero;

    private void FixedUpdate()
    {
        transform.position += _direction * speed * Time.deltaTime;
    }

    protected override bool CanHit(Collider other)
    {
        return other.tag != owner.tag && (other.tag == EnemyData.TAG || other.tag == PlayerData.TAG);
    }

    public override void Hit(ICharacter character, bool isMelee = true)
    {
        base.Hit(character, false);

        if (impactFx != null)
        {
            GameObject impact = GamePoolManager.instance.UseFromPool(impactFx.name);

            impact.transform.position = transform.position;
            impact.transform.rotation = transform.rotation;

            impact.GetComponent<VisualEffectController>().Reset(owner.currentState.flag);
        }

        if (destroyAtImpact == true)
        {
            Destroy();
        }
    }

    public override void Attach(CharacterFSM character)
    {
        base.Attach(character);

        _direction = transform.forward;

        if (character.tag == PlayerData.TAG)
        {
            PlayerFSM player = (PlayerFSM)character;

            if (player.target != null)
            {
                EnemyFSM enemy = (EnemyFSM)player.target;

                _direction = enemy.stateHit.anchor.position - player.stateAttack.anchor.position;
                _direction.Normalize();
            }
        }
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}