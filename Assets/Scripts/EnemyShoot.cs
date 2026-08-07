using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float timeBetweenShots = 2f;

    private float nextShotTime = 0f;

    public void TryShoot(Transform target)
    {
        if (Time.time < nextShotTime)
        {
            return;
        }

        Shoot(target);

        nextShotTime =
            Time.time + timeBetweenShots;
    }

    void Shoot(Transform target)
    {
        if (
            projectilePrefab == null ||
            firePoint == null ||
            target == null
        )
        {
            return;
        }

        Vector3 direction =
            target.position - firePoint.position;

        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
        {
            return;
        }

        Quaternion rotation =
            Quaternion.LookRotation(direction.normalized);

        Instantiate(
            projectilePrefab,
            firePoint.position,
            rotation
        );
    }
}