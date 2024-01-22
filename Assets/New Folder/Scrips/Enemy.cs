using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int damageValue = 10; // Adjust this value based on the damage the enemy should deal

    // Getter method for retrieving the damage value
    public int GetDamageValue()
    {
        return damageValue;
    }

    // Method to handle taking damage
    public void TakeDamage(int damage, PlayerManager player)
    {
        // Implement logic to handle taking damage here
        // For example, reduce health by the damage amount
        player.TakeDamage(damage);
    }
}
