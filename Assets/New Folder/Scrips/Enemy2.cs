using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public int enemyHP = 100;
    public GameObject projectile;
    public Transform projectilePoint;

    public Animator animator;

    public void Shoot()
    {
        Rigidbody rb = Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 30f, ForceMode.Impulse);
        rb.AddForce(transform.up * 3, ForceMode.Impulse);
    }

    public void TakeDamage(int damageAmount)
    {
        enemyHP -= damageAmount;
        if (enemyHP <= 0)
        {
            animator.SetTrigger("death");
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }
    private void Die()
    {
        // Trigger the "death" animation
        animator.SetTrigger("death");
        Debug.Log("Death Animation Triggered");

        // Additional debug logs
        Debug.Log("Enemy is dying!");

        // Perform other actions when the enemy dies, such as disabling colliders, etc.
        Destroy(gameObject);
    }

}