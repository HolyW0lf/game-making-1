using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject bulletPredab;
    public Transform shootingpoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
             GameObject bullet =  Instantiate(bulletPredab, shootingpoint.position, Quaternion.identity );
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 5000f);
        }   
    }
}
