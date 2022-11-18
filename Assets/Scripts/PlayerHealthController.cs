using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public float maxHealth, curHealth;
    public bool canDamage;
    public bool godmode;

    public Image damagePanel;

    public GameObject blockEffect;
    public Transform blockPos;

    private Animator anim;
    private PlayerCombatController pCombat;
    public float stamina;
    public Scrollbar healthBar, staminaBar;

    void Start()
    {
        anim = GetComponent<Animator>();
        pCombat = GetComponent<PlayerCombatController>();
        curHealth = maxHealth;
    }

    void Update()
    {
        staminaBar.size = stamina;

        if (stamina < 1 && !pCombat.isBlocking)
        {
            stamina += Time.deltaTime / 5;
        }
        else if(stamina > 1)
        {
            stamina = 1;
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            curHealth -= 100;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if(godmode)
            {
                godmode = false;
            }
            else
            {
                godmode = true;
            }
        }

        if (curHealth <= 0)
        {
            GameManager.instance.GameOver();
        }

        healthBar.GetComponent<Scrollbar>().size = curHealth / 100;

        if (damagePanel.color.a > 0)
        {
            damagePanel.color = new Color(1, 0, 0, Mathf.MoveTowards(damagePanel.color.a, 0, .5f * Time.deltaTime));
        }
    }

    public void DealDamage()
    {
        if(canDamage && !godmode)
        {
            curHealth -= 10;

            damagePanel.color = new Color(1, 0, 0, .2f);
        }
        else if(!canDamage)
        {
            Instantiate(blockEffect, blockPos.position, blockPos.rotation);
            stamina -= 0.1f;
            anim.Play("PlayerBlocked");
        }
    }

    public void AddHealth()
    {
        if(curHealth >= maxHealth)
        {

        }
        else
        {
            curHealth += 20;
        }
    }
}
