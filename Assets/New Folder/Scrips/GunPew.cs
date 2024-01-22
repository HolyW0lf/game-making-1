using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
public class Gun : MonoBehaviour
{
    private AudioManager audioManager;

    public Sound[] sounds;

    public Transform fpsCam;
    public float range = 20;
    public float impactForce = 150;
    public int damageAmount = 20;

    public int fireRate = 10;
    private float nextTimeToFire = 0;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    public int currentAmmo;
    public int maxAmmo = 10;
    public int magazineAmmo = 30;

    public float reloadTime = 2f;
    public bool isReloading;

    InputAction shoot;

    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene.");
        }

        shoot = new InputAction("Shoot", binding: "<mouse>/leftButton");
        shoot.AddBinding("<Gamepad>/x");

        shoot.Enable();

        currentAmmo = maxAmmo;
    }

    private void OnEnable()
    {
        isReloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAmmo == 0 && magazineAmmo == 0)
        {
            return;
        }

        if (isReloading)
            return;

        // Check if the shooting action is in progress
        bool isShooting = shoot.ReadValue<float>() > 0.5f;

        // Check if the shooting state has changed
        if (isShooting && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Fire();
        }

        if (currentAmmo == 0 && magazineAmmo > 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);

        if (s != null && s.source != null)
        {
            Debug.Log("Playing sound: " + sound);
            s.source.Play();
        }
        else
        {
            Debug.LogWarning("Sound with name " + sound + " not found or AudioSource is null.");
        }
    }

    private void Fire()
    {
        Debug.Log("Firing!");

        if (muzzleFlash != null && !muzzleFlash.isPlaying)
        {
            muzzleFlash.Play();
        }

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.position + fpsCam.forward, fpsCam.forward, out hit, range))
        {
            Enemy2 enemy2 = hit.transform.GetComponent<Enemy2>();
            if (enemy2 != null)
            {
                enemy2.TakeDamage(damageAmount);
                enemy2.Shoot(); // Call the Shoot method when hitting an enemy
                Debug.Log("Hit enemy: " + enemy2.gameObject.name);
                return;
            }

            Debug.Log("Hit: " + hit.transform.name);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            // Additional handling for other enemy types can be added here
            // ...

            Quaternion impactRotation = Quaternion.LookRotation(hit.normal);
            GameObject impact = Instantiate(impactEffect, hit.point, impactRotation);

            // Check if the ParticleSystem component is present before accessing it
            ParticleSystem particleSystem = impact.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.transform.parent = hit.transform;
                Destroy(impact, 5);
            }
            else
            {
                Debug.LogWarning("Impact effect is missing ParticleSystem component.");
            }
        }

        StartCoroutine(StopMuzzleFlash());
        audioManager?.Play("Shoot"); // Using the null-conditional operator to avoid null reference
    }


    IEnumerator StopMuzzleFlash()
    {
        yield return new WaitForSeconds(0.1f);

        if (muzzleFlash != null)
        {
            muzzleFlash.Stop();
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        AudioManager.instance.Play("Reload");
        yield return new WaitForSeconds(reloadTime);

        if (magazineAmmo >= maxAmmo)
        {
            currentAmmo = maxAmmo;
            magazineAmmo -= maxAmmo;
        }
        else
        {
            currentAmmo = magazineAmmo;
            magazineAmmo = 0;
        }

        isReloading = false;
    }
}
