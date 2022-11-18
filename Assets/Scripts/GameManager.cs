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
                int randomEnemy = Random.Range(0, enemies.Length);

                Instantiate(enemies[randomEnemy], spawners[randomSpawner].transform.position, Quaternion.identity);
                spawnCounter = timeToSpawn;
                isSpawning = false;
            }
        }
    }
}
