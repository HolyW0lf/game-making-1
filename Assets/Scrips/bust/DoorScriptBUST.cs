using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private bool doorState = false;
    private bool inRange = false;     
   
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && inRange)
        {
            TaggleDoorState();
        }
    }

    private void TaggleDoorState()
    {
        doorState = !doorState;
        animator.SetBool("open", doorState);

    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" ) inRange = true;    

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player") inRange = false;

    }









}
