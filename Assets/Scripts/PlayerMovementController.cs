using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public CharacterController controller;

    public float moveSpeed = 4f;
    public float gravity = -9.81f;
    public float jumpForce = 1.5f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask whatIsGroung;

    Vector3 velocity;
    bool isGrounded;

    private PlayerHealthController playerHealth;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealthController>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGroung);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        controller.Move(move * moveSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2 * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
