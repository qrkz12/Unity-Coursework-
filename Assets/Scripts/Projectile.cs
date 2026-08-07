using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 12f;
    public int damage = 25;
    public float lifetime = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.linearVelocity = transform.forward * speed;

        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        EnemyHealth enemyHealth =
            collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}