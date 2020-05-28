using UnityEngine;

public class ProjectileController : WeaponController
{
    public float speed = 0f;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public override void Hit(ICharacter character)
    {
        base.Hit(character);

        Destroy(gameObject);
    }
}