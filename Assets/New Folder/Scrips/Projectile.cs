using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed = 10f;
    public int damageAmount = 5;

    private void Start()
    {
        Destroy(gameObject, 3f); // Destroy the bullet after 3 seconds (adjust as needed)
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision with Player");
            DealDamageToPlayer(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void DealDamageToPlayer(GameObject player)
    {
        PlayerManager playerManager = player.GetComponent<PlayerManager>();
        if (playerManager != null)
        {
            StartCoroutine(playerManager.TakeDamage(damageAmount));
        }
    }
}
