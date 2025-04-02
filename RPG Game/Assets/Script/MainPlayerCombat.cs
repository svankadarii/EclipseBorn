using UnityEngine;
using UnityEngine.Timeline;
using System.Collections;

public class MainPlayerCombat : MonoBehaviour
{
    private Animator animator;
    private int comboCount = 0;
    private float comboTimer = 0f;
    private float comboWindow = 1f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        animator.applyRootMotion = true;
    }

    void Update()
    {
        
        if (comboCount > 0)
        {
            comboTimer += Time.deltaTime;

            
            if (comboTimer > comboWindow)
            {
                ResetCombo();
            }
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        
        comboTimer = 0f;

        
        comboCount++;

        
        switch (comboCount)
        {
            case 1:
                animator.SetTrigger("Attack1");
                Vector3 position = animator.rootPosition;
                controller.Move(position - transform.position);
                transform.rotation = animator.rootRotation;
                break;
            case 2:
                animator.SetTrigger("Attack2");
                
                break;
            case 3:
                animator.SetTrigger("Attack3");
                break;
            default:
                ResetCombo();
                break;
        }
    }

    void ResetCombo()
    {
        comboCount = 0;
        comboTimer = 0f;
        animator.SetTrigger("NoAttack");
    }
}