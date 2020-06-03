using UnityEngine;

public class ProjectileController : WeaponController
{
    public float speed = 0f;
    public GameObject impactFx = null;
    private Vector3 _direction = Vector3.zero;

    private void FixedUpdate()
    {
        transform.position += _direction * speed * Time.deltaTime;
    }

    public override void Hit(ICharacter character)
    {
        base.Hit(character);

        if (impactFx != null)
        {
            Instantiate<GameObject>(impactFx, transform.position, transform.rotation);
        }

        GameObject.Destroy(gameObject);
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
}