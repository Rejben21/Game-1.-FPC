using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public float maxHealth, curHealth;
    public bool canDamage;
    public bool godmode;

    public Scrollbar healthBar;

    void Start()
    {
        curHealth = maxHealth;
    }

    void Update()
    {
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
    }

    public void DealDamage()
    {
        if(canDamage && !godmode)
        {
            curHealth -= 10;
        }
        else
        {

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
