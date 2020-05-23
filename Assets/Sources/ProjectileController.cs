using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 0f;
    public int damage = 0;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<EnemyFSM>().Hit(damage);

        Destroy(gameObject);
    }
}