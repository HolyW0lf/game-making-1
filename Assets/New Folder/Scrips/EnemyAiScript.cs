using UnityEngine;
using UnityEngine.AI;

public class EnemyAIScript : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 10f;
    public float timeBetweenShots = 1f;
    public float detectionRadius =25f; // Radius for detecting the player
    public float shootingRadius = 15f;   // Radius for shooting the player

    private Gun gunScript;
    private float timeSinceLastShot;
    private float health = 400f; // Initial health value, adjust as needed

    private NavMeshAgent navMeshAgent;
    private bool isChasing = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gunScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Rotate towards the player
        transform.LookAt(player);

        // Shooting logic
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= timeBetweenShots && distanceToPlayer <= shootingRadius)
        {
            Shoot();
            timeSinceLastShot = 0f;
        }

        // Check for player detection
        if (distanceToPlayer <= detectionRadius)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        // Chase the player
        if (isChasing)
        {
            ChasePlayer();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.velocity = transform.forward * bulletSpeed;
        }
        else
        {
            Debug.LogError("Bullet prefab is missing Rigidbody component.");
        }
    }

    private void ChasePlayer()
    {
        // Set the destination of the NavMeshAgent to the player's position
        navMeshAgent.SetDestination(player.position);
    }

    // Method to handle taking damage
    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
        {
            Die();
        }
        else
        {
            Debug.Log("Enemy took damage: " + damage + ", Remaining health: " + health);
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }
}
