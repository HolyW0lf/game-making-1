using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damageAmount = 10;
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
     private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
       
        if (other.tag == "enemy")
        {
            other.GetComponent<Enemay>().TakeDamage(damageAmount);
        }
    }
}
