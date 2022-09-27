using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character_Mei : MonoBehaviour
{
    private const float BATTLE_TIME_START = 5.0f;
    private const float BATTLE_TIME_END = 0.0f;
    // Component
    private Rigidbody rigid = null;
    private Animator animator = null;

    // MoveSpeed
    private float moveSpeed = 1.5f;
    private float spinSpeed = 360.0f;
    // MoveDirection
    private float moveInput = 0.0f;
    private float spinInput = 0.0f;

    private float battleTime = BATTLE_TIME_START;
    private bool isBattle = false;
    private bool isComboEnd = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        // Move를 통해 받은 입력방향으로 이동
        Move();
    }
    private void Update()
    {
        BattleTimeCheck();
        for (int i = 1; i < 3; i++)
        {
            if (animator.GetCurrentAnimatorStateInfo(2).IsName("Attack"))
            {
                Debug.Log("11");
                animator.applyRootMotion = true;
            }
            else
            {
                animator.applyRootMotion = false;
            }
        }
    }
    // 전투 비전투 모드 전환
    private void BattleTimeCheck()
    {
        if (isBattle == true)
        {
            battleTime -= Time.deltaTime;
            if (battleTime < BATTLE_TIME_END)
            {
                isBattle = false;
                battleTime = BATTLE_TIME_START;
                animator.SetBool("isBattle", false);
            }
        }
    }
    public void AttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {            
            animator.SetTrigger("isAttack");
        }
        if (context.canceled)
        {
            battleTime = BATTLE_TIME_START;
        }
    }

    // 움직임 관련
    private void Move()
    {
        rigid.MovePosition(rigid.position + transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime);
        rigid.MoveRotation(rigid.rotation * Quaternion.AngleAxis(spinInput * spinSpeed * Time.fixedDeltaTime, Vector3.up));
    }
    public void MoveInput(InputAction.CallbackContext context)
    {
        Vector2 readKey = context.ReadValue<Vector2>();
        moveInput = readKey.y;
        spinInput = readKey.x;

        animator.SetFloat("MoveDir", moveInput);

        if (context.canceled)
        {
            animator.SetBool("isRun", false);
            moveSpeed = 1.5f;
        }
    }
    public void RunInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (moveInput > 0)
            {
                animator.SetBool("isRun", true);
                moveSpeed = 3.0f;
            }
        }
    }
}
