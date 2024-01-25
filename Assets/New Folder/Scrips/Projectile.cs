using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject impactEffect;
    

        public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile collided with something");
        FindObjectOfType<AudioManager>().Play("Explosion");
        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(impact, 2);

        
          

        this.enabled = false;
    }
}
