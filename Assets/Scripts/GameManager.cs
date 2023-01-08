using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timeToSpawn;
    private float spawnCounter;
    public bool hasStarted, isSpawning;
    public GameObject menu;

    public float waveTime;
    private float waveCounter;

    public GameObject[] spawners;
    public GameObject[] enemies;

    public List<EnemyHealthController> enemiesAlive = new List<EnemyHealthController>();

    public Text endText;
    public GameObject endGame;

    public Text waveText;
    public int wave;

    private int minEnemy = 0;
    private int maxEnemy = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 0;
        spawnCounter = timeToSpawn;
        waveCounter = waveTime;
    }

    private void Update()
    {
        waveCounter -= Time.deltaTime;

        if(waveCounter > 0)
        {
            Spawn();
        }
        else if(waveCounter <= 0 && enemiesAlive.Count == 0)
        {
            wave++;
            waveText.text = wave.ToString();
            waveTime = waveTime + 5;
            waveCounter = waveTime;
        }
        
        if(enemiesAlive.Count == 0 && wave >= 6)
        {
            hasStarted = false;
            Victory();
        }

        switch(wave)
        {
            case 1:
                minEnemy = 0;
                maxEnemy = 1;
                break;

            case 2:
                minEnemy = 0;
                maxEnemy = 2;
                break;

            case 3:
                minEnemy = 0;
                maxEnemy = 3;
                break;

            case 4:
                minEnemy = 0;
                maxEnemy = 4;
                break;

            case 5:
                minEnemy = 1;
                maxEnemy = 4;
                break;
        }
    }

    public void Victory()
    {
        Time.timeScale = 0.1f;
        waveText.text = "5";
        endText.text = "Victory! :D";
        endGame.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        endText.text = "Game Over... :/";
        endGame.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        hasStarted = true;
        wave++;
        waveText.text = wave.ToString();
        Cursor.lockState = CursorLockMode.Locked;
        menu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void Spawn()
    {
        if (hasStarted)
        {
            spawnCounter -= Time.deltaTime;
            if (spawnCounter <= 0)
            {
                isSpawning = true;
            }

            if (isSpawning)
            {
                int randomSpawner = Random.Range(0, spawners.Length);
                int randomEnemy = Random.Range(minEnemy, maxEnemy);

                Instantiate(enemies[randomEnemy], spawners[randomSpawner].transform.position, Quaternion.identity);
                spawnCounter = timeToSpawn;
                isSpawning = false;
            }
        }
    }
}
