using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Attack();
        }
    }

    void Attack()
    {
        GameObject opponent = GameObject.FindGameObjectWithTag("Opponent");

        if (opponent != null)
        {
            // Deal damage to the opponent (assuming they have a health script)
            opponent.GetComponent<OpponentHealth>().TakeDamage(10f);
        }
        // Put your attack logic here
        Debug.Log("Attack!");
    }
}

