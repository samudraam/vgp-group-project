using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints; // Assign in inspector
    public TMP_Text enemyCounterText;

    private int currentWave = 0;
    private int totalWaves = 2;
    private int enemiesPerWave = 2;
    private int enemiesRemaining = 0;

    private List<GameObject> activeEnemies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    void Update()
    {
        activeEnemies.RemoveAll(enemy => enemy == null);

        enemiesRemaining = activeEnemies.Count;
        enemyCounterText.text = $"Enemies Left: {enemiesRemaining}";

        if (enemiesRemaining == 0)
        {
            if (currentWave < totalWaves)
            {
                StartCoroutine(SpawnWave());

            }
            else
            {
                Debug.Log("You win!");
                FindObjectOfType<UIController>()?.WinGame();
            }
        }
    }

    IEnumerator SpawnWave()
    {
        currentWave++;
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            activeEnemies.Add(newEnemy);

            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("Wave " + currentWave + " spawned.");
    }
}
