using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private Animator animator;
    private bool isFirstAttack = true;
    private bool isBlocking;
    private bool isAttacking;
    public GameObject axePrefab;
    public Transform mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && !isBlocking)
        {
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            isBlocking = true;
            GetComponent<PlayerHealthController>().canDamage = false;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            isBlocking = false;
            GetComponent<PlayerHealthController>().canDamage = true;
        }

        if(Input.GetKeyDown(KeyCode.E) && !isAttacking && !isBlocking)
        {
            Instantiate(axePrefab, transform.position, mainCamera.rotation);
        }

        animator.SetBool("IsBlocking", isBlocking);
    }

    private void Attack()
    {
        isAttacking = true;

        if(isFirstAttack)
        {
            animator.Play("PlayerFirstAttack");
        }
        else
        {
            animator.Play("PlayerSecondAttack");
        }
    }

    private void EndAttack()
    {
        isAttacking = false;

        if(isFirstAttack)
        {
            isFirstAttack = false;
        }
        else
        {
            isFirstAttack = true;
        }
    }
}
