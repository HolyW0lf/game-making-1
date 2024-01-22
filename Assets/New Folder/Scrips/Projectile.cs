using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject impactEffect;
    public float radius = 3;
    public int damageAmount = 15;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile collided with something");
        FindObjectOfType<AudioManager>().Play("Explosion");
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, 2);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Player"))
            {
                PlayerManager playerManager = nearbyObject.GetComponent<PlayerManager>();
                if (playerManager != null)
                {
                    // Call the TakeDamage method when the player is hit
                    playerManager.TakeDamage(damageAmount);
                    Debug.Log("Player takes damage: " + damageAmount);
                }
            }
        }

        this.enabled = false;
    }
}
