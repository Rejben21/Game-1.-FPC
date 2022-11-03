using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timeToSpawn;
    public bool hasStarted, isSpawning;
    public GameObject menu;

    public GameObject[] spawners;
    public GameObject[] enemies;
    public EnemyController[] enemiesAlive;

    public float waveTime = 15;

    public Text endText;
    public GameObject endGame;

    public Text waveText;
    public int wave = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void Update()
    {
        if(enemiesAlive == null)
        {

        }
        enemiesAlive = FindObjectsOfType<EnemyController>();

        waveText.text = wave.ToString();

        if (!isSpawning && hasStarted)
        {
            StartCoroutine(Wave1());
        }

        if(hasStarted)
        {
            waveTime -= Time.deltaTime;
        }

        if(waveTime <= 0 && wave == 1)
        {
            StopAllCoroutines();
            wave++;
            waveTime += 25;
            StartCoroutine(Wave2());
        }

        if (waveTime <= 0 && wave == 2)
        {
            StopAllCoroutines();
            wave++;
            waveTime += 30;
            StartCoroutine(Wave3());
        }

        if (waveTime <= 0 && wave == 3)
        {
            StopAllCoroutines();
            wave++;
            waveTime += 35;
            StartCoroutine(Wave4());
        }

        if (waveTime <= 0 && wave == 4)
        {
            StopAllCoroutines();
            wave++;
            waveTime += 40;
            StartCoroutine(Wave5());
        }

        if (waveTime <= 0 && wave == 5)
        {
            StopAllCoroutines();
        }

        if (waveTime <= 0 && wave == 5 && enemiesAlive.Length <= 0)
        {
            Victory();
        }
    }

    public void Victory()
    {
        Time.timeScale = 0;
        endText.text = "Victory";
        endGame.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        endText.text = "GameOver";
        endGame.SetActive(false);
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        wave++;
        hasStarted = true;
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

    IEnumerator Wave1()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeToSpawn);

        int randomSpawner = Random.Range(0, 6);

        Instantiate(enemies[0], spawners[randomSpawner].transform.position, Quaternion.identity);
        isSpawning = false;
    }
    IEnumerator Wave2()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeToSpawn);

        int randomEnemy = Random.Range(0, 1);
        int randomSpawner = Random.Range(0, 6);

        Instantiate(enemies[randomEnemy], spawners[randomSpawner].transform.position, Quaternion.identity);
        isSpawning = false;
    }
    IEnumerator Wave3()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeToSpawn);

        int randomEnemy = Random.Range(0, 2);
        int randomSpawner = Random.Range(0, 6);

        Instantiate(enemies[randomEnemy], spawners[randomSpawner].transform.position, Quaternion.identity);
        isSpawning = false;
    }
    IEnumerator Wave4()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeToSpawn);

        int randomEnemy = Random.Range(0, 3);
        int randomSpawner = Random.Range(0, 6);

        Instantiate(enemies[randomEnemy], spawners[randomSpawner].transform.position, Quaternion.identity);
        isSpawning = false;
    }
    IEnumerator Wave5()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeToSpawn / 2);

        int randomEnemy = Random.Range(1, 3);
        int randomSpawner = Random.Range(0, 6);

        Instantiate(enemies[randomEnemy], spawners[randomSpawner].transform.position, Quaternion.identity);
        isSpawning = false;
    }
}
