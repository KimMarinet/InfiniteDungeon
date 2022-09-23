using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Mei : MonoBehaviour
{
    //
    private Rigidbody rigidbody = null;
    private Animator animator = null;

    // Move
    private float moveSpeed = 3.0f;
    private float spinSpeed = 360.0f;
    private float moveInput = 0.0f;
    private float spinInput = 0.0f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime);
        rigidbody.MoveRotation(rigidbody.rotation * Quaternion.AngleAxis(spinInput * spinSpeed * Time.fixedDeltaTime, Vector3.up));
    }
    private void Update()
    {

    }
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 readKey = context.ReadValue<Vector2>();
        moveInput = readKey.y;
        spinInput = readKey.x;

        if (context.started)
        {
            if(moveInput > 0)
            {
                animator.SetBool("Run", true);
                moveSpeed = 3.0f;
            }
            else
            {
                animator.SetBool("Walk_B", true);
                moveSpeed = 1.5f;
            }
        }
        if (context.canceled)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Walk_B", false);
        }
    }
}
