using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 30f;
    public float timeBetweenShots = 0.5f;
    public float attackRange = 10f; // Set this to the attack range of your enemy

    private float timeSinceLastShot;

    private void Update()
    {
        // Check if the player is within attack range
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            // Rotate towards the player
            transform.LookAt(player);

            // Shooting logic
            timeSinceLastShot += Time.deltaTime;

            if (timeSinceLastShot >= timeBetweenShots)
            {
                Shoot();
                timeSinceLastShot = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            // Set the bullet's velocity
            bulletRb.velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
        else
        {
            Debug.LogError("Bullet prefab is missing Rigidbody component.");
        }
    }
}
