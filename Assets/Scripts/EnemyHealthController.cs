using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public int maxHealth, dealDamage;
    [HideInInspector]
    public int curHealth;
    public bool isMage;
    private float timeToHeal = 1;

    public GameObject healthBar;
    private PlayerMovementController player;

    public GameObject[] dismembermentElements;
    public Transform[] dismembermentTransforms;

    public GameObject[] pickUps;

    void Start()
    {
        curHealth = maxHealth;
        player = FindObjectOfType<PlayerMovementController>();

        GameManager.instance.enemiesAlive.Add(this);
    }

    void Update()
    {
        if(curHealth <= 0)
        {
            EnemyDeath();
        }

        if(curHealth > 0 && curHealth < maxHealth)
        {
            Heal();
        }

        HealthBarController();
    }

    public void HealthBarController()
    {
        if (curHealth == maxHealth || curHealth <= 0)
        {
            healthBar.SetActive(false);
        }
        else
        {
            healthBar.SetActive(true);
        }

        if (healthBar != null)
        {
            healthBar.transform.LookAt(player.transform);
            healthBar.transform.rotation = Quaternion.Euler(0, healthBar.transform.rotation.eulerAngles.y, 0);

            healthBar.GetComponent<Slider>().value = curHealth;
        }
    }

    public void DealDamage()
    {
        curHealth -= dealDamage;
    }

    private void Heal()
    {
        if(isMage && curHealth < maxHealth)
        {
            timeToHeal -= Time.deltaTime;

            if(timeToHeal <= 0)
            {
                curHealth++;
                timeToHeal = 1;
            }
        }
    }

    private void EnemyDeath()
    {
        Instantiate(dismembermentElements[0], dismembermentTransforms[0].position, dismembermentTransforms[0].rotation);
        Instantiate(dismembermentElements[1], dismembermentTransforms[1].position, dismembermentTransforms[1].rotation);
        Instantiate(dismembermentElements[2], dismembermentTransforms[2].position, dismembermentTransforms[2].rotation);
        Instantiate(dismembermentElements[3], dismembermentTransforms[3].position, dismembermentTransforms[3].rotation);
        Instantiate(dismembermentElements[4], dismembermentTransforms[4].position, dismembermentTransforms[4].rotation);

        int randomValue = Random.Range(0, 100);
        if(randomValue < 15)
        {
            Instantiate(pickUps[0], transform.position, Quaternion.identity);
        }
        else if (randomValue > 15 && randomValue < 30)
        {
            Instantiate(pickUps[1], transform.position, Quaternion.identity);
        }
        else
        {

        }

        GameManager.instance.enemiesAlive.Remove(this);
        Destroy(gameObject);
    }
}
