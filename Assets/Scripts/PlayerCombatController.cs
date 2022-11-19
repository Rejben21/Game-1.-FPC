using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombatController : MonoBehaviour
{
    private Animator animator;
    private PlayerHealthController pHealth;
    private bool isFirstAttack = true;
    [HideInInspector]
    public bool isBlocking;
    private bool isAttacking;
    public GameObject axePrefab;
    public Transform mainCamera;
    public float timeToThrowAxe;
    private float throwCounter;
    public GameObject axeThrowTimer;
    public GameObject bombPrefab;
    public Text bombsText;
    public int bombs = 3;
    private float isAttackingTimer = 2f;
    private float AttackingCounter;

    void Start()
    {
        animator = GetComponent<Animator>();
        pHealth = GetComponent<PlayerHealthController>();
        throwCounter = timeToThrowAxe;
        axeThrowTimer.GetComponent<Slider>().maxValue = timeToThrowAxe;
        AttackingCounter = isAttackingTimer;
    }

    void Update()
    {
        bombsText.text = " " + bombs.ToString() + "/3";

        if(isAttacking)
        {
            AttackingCounter -= Time.deltaTime;
        }

        if (AttackingCounter <= 0)
        {
            isAttacking = false;
            AttackingCounter = isAttackingTimer;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && !isBlocking)
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && pHealth.stamina > 0)
        {
            isBlocking = true;
            pHealth.canDamage = false;
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1) || pHealth.stamina <= 0)
        {
            isBlocking = false;
            pHealth.canDamage = true;
        }

        ThrowAxe();
        BombThrow();

        animator.SetBool("IsBlocking", isBlocking);
    }

    private void BombThrow()
    {
        if (Input.GetKeyDown(KeyCode.Q) && bombs > 0 && !isAttacking && !isBlocking)
        {
            Instantiate(bombPrefab, transform.position, mainCamera.rotation);
            bombs--;
        }

        if(bombs <= 0)
        {
            bombs = 0;
        }
    }

    private void ThrowAxe()
    {
        if (Input.GetKeyDown(KeyCode.E) && throwCounter == timeToThrowAxe && !isAttacking && !isBlocking)
        {
            Instantiate(axePrefab, transform.position, mainCamera.rotation);
            throwCounter = 0;
        }

        if (throwCounter < timeToThrowAxe)
        {
            throwCounter += Time.deltaTime;
            axeThrowTimer.SetActive(true);
        }

        if (throwCounter > timeToThrowAxe)
        {
            throwCounter = timeToThrowAxe;
            axeThrowTimer.SetActive(false);
        }

        axeThrowTimer.GetComponent<Slider>().value = throwCounter;
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
