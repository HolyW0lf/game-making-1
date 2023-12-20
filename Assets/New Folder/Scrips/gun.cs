using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public Transform bulletSpawn;
    public Rigidbody bullet;
    public float bulletSpeed;


    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody bulletrigidbody;
            bulletrigidbody = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation)as Rigidbody;
            bulletrigidbody.AddForce(bulletSpawn.forward * bulletSpeed);






        }
    }
}
