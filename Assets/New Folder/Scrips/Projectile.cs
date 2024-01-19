using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody rb;
    private Transform target;

    // Method to initialize the projectile
    public void ShootAt(Transform targetTransform)
    {
        target = targetTransform;

        // Set the initial position to match the enemy's height
        transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        transform.LookAt(target.position);

        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        Destroy(gameObject, 3f); // Destroy the bullet after 3 seconds (adjust as needed)
    }


    private void OnTriggerEnter(Collider other)
    {
        // Example: Destroy the bullet when it hits something
        if (other.CompareTag("Player"))
        {
            // Do damage or other actions as needed
            Destroy(gameObject);
        }
    }
}
