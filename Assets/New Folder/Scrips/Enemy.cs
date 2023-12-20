using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemay : MonoBehaviour
{
    public int HP = 100;
    public Animator animator;

   
    public void TakeDamage(int DamageAmount)
    {
        HP -= DamageAmount;
        if (HP <= 0)
        {
            AudioManager.instance.Play("enemy_die");
            animator.SetTrigger("Die");
            GetComponent<Collider>().enabled = false;

        }
        else
        {
            AudioManager.instance.Play("enemy_hit");
            animator.SetTrigger("Damage");

        }
    }


}
