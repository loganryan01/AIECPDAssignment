﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo3Script : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 1.0f;
    public float maximumRotation = 90.0f;
    public float gravity = -9.81f;

    public Animator robo3Controller;
    public CharacterController robo3CharacterController;
    public Robo2Script enemyScript;

    private Vector3 moveDirection;
    private Vector3 velocity;

    public bool turning = false;
    public bool isAttacking;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        robo3CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float jumpInput = Input.GetAxis("Jump");
        float fireInput = Input.GetAxis("Fire3");

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }

        if (horizontalInput != 0 || verticalInput != 0)
        {
            robo3Controller.SetBool("Run", true);
        }
        else
        {
            robo3Controller.SetBool("Run", false);
        }

        Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
        robo3CharacterController.Move(move * speed * Time.deltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (isGrounded && jumpInput == 1)
        {
            velocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravity);
            isGrounded = false;
        }
        velocity.y += gravity * Time.deltaTime;

        robo3CharacterController.Move(velocity * Time.deltaTime);

        if (fireInput == 1)
        {
            robo3Controller.SetBool("Attack", true);
            isAttacking = true;
        }

        if (robo3Controller.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            robo3Controller.SetBool("Attack", false);
            isAttacking = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy") && isAttacking)
        {
            Destroy(hit.gameObject);
        }

        if (hit.gameObject.CompareTag("Enemy") && !isGrounded)
        {
            moveDirection.y = jumpForce;
            enemyScript = hit.gameObject.GetComponent<Robo2Script>();
            enemyScript.isDead = true;
        }

        if (hit.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (hit.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = true;

            transform.parent = hit.transform;
        }
    }
}
