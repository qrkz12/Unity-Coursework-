using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 10;
    public float lifetime = 5f;

    public GameObject explosionPrefab;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.linearVelocity =
            transform.forward * speed;

        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealth =
            collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }

        if (explosionPrefab != null)
        {
            Instantiate(
                explosionPrefab,
                transform.position,
                Quaternion.identity
            );
        }

        Destroy(gameObject);
    }
}