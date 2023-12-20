using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool doorState = false;
    private bool inRange = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && inRange)
        {
            ToggleDoorState();
        }
            
    }
    
    private void ToggleDoorState()
    {
        doorState = !doorState;
        animator.SetBool("DoorState", doorState);
            
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") inRange = true;

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player") inRange = false;

    }




}
