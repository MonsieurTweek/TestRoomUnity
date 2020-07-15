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

    public override void Hit(ICharacter character)
    {
        base.Hit(character);

        if (impactFx != null)
        {
            Instantiate<GameObject>(impactFx, transform.position, transform.rotation);
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
                _direction = player.target.transform.position - player.transform.position + Vector3.up;
                _direction.Normalize();
            }
        }
    }

    public void Destroy()
    {
        GameObject.Destroy(gameObject);
    }
}