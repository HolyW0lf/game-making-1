using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    public int playerHealth = 100;
    public TextMeshProUGUI healthText; // TextMeshPro Text for displaying health

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Assuming you've assigned the TextMeshPro Text in the Unity Editor
        healthText.text = "Health: " + playerHealth;
    }

    void Update()
    {
        #region Movement
        if (canMove)
        {
            float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
            Vector3 move = transform.TransformDirection(moveDirection) * moveSpeed;

            // Apply gravity
            move.y -= gravity * Time.deltaTime;

            // Move the character controller
            characterController.Move(move * Time.deltaTime);
        }
        #endregion



        #region Rotation
        if (canMove)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion

        #region Collision Detection and Damage
        // Check if the character controller is colliding with something
        if (characterController.isGrounded)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, characterController.radius);
            foreach (Collider col in colliders)
            {
                // Check if the collided object has an Enemy component
                Enemy enemy = col.GetComponent<Enemy>();
                if (enemy)
                {
                    // Perform actions specific to colliding with an enemy
                    int damageValue = enemy.GetDamageValue();

                    // Apply damage to the player's health
                    playerHealth -= damageValue;

                    // Ensure health does not go below 0
                    playerHealth = Mathf.Max(0, playerHealth);

                    // Update the health text display
                    healthText.text = playerHealth.ToString();

                    // Check if player's health is depleted, implement game over logic if needed
                    if (playerHealth == 0)
                    {
                        // Game over logic
                    }
                }
            }
        }
        #endregion
    }
}
