using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static int playerHP = 100;
    public TextMeshProUGUI playerHPText;
    public GameObject bloodOverlay;
    public GameObject nearbyObject;
    public static bool isGameOver;
    public int damageAmount = 15;
    public float radius = 50;

    void Start()
    {
        isGameOver = false;
        playerHP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (nearbyObject.CompareTag("Player"))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider nearbyObject in colliders)
            {
                ProjectileScript projectileScript = nearbyObject.GetComponent<ProjectileScript>();
                if (projectileScript != null)
                {
                    // Call the TakeDamage method when the player is hit
                    StartCoroutine(TakeDamage(damageAmount));
                    Debug.Log("Player takes damage: " + damageAmount);
                }
            }
            playerHPText.text = "Health: 100 " + playerHP.ToString();
        }

        if (isGameOver)
        {
            SceneManager.LoadScene("Level");
        }
    }

    public IEnumerator TakeDamage(int damageAmount)
    {
        bloodOverlay.SetActive(true);
        playerHP -= damageAmount;
        if (playerHP <= 0)
        {
            isGameOver = true;
        }

        yield return new WaitForSeconds(1);
        bloodOverlay.SetActive(false);

        Debug.Log("Player's health: " + playerHP);
    }
}
