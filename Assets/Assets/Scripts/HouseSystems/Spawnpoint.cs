using UnityEngine;
using UnityEngine.UI;

public class Spawnpoint : MonoBehaviour
{
    #region

    [SerializeField] private GameObject[] ennemysToSpawn;
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private int waveNumber = 1;
    [SerializeField] private float timeBetweenWaves = 25f;
    TowerSpawner towerSpawner;

    #endregion

    #region

    public Text waveCountdownText;
    public Text waveNumberText;

    #endregion

    #region

    private float countdown = 2f;
    private int enemiesAlive;
    private bool isSpawning = false;
    private int currentWave;
    private float waveTimer = 25f;
    private int enemiesPerWave;
    private int maxWaves = 25;

    #endregion

    private void Start()
    {
        startWave();
    }
    private void Update()
    {
        waveNumberText.text = "Wave: " + currentWave + "/" + maxWaves;
        waveCountdownText.text = "current enemies" + enemiesPerWave;
        if (enemiesAlive > 0)
        {
            startWave();
            enemiesPerWave++;

        }

    }
    void startWave()
    {
        isSpawning = true;
        currentWave++;
        enemiesPerWave += 5;
        waveTimer = timeBetweenWaves;
        SpawnEnemies();
    }

    public void ennemydeath()
    {

        enemiesAlive--;
        towerSpawner.BroadcastMessage("addMoney", 10);
    }
    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawnPoint = spawnLocations[Random.Range(0, spawnLocations.Length)];
            GameObject enemyPrefab = ennemysToSpawn[Random.Range(0, ennemysToSpawn.Length)];
            waveCountdownText.text = "Spawning Enemy " + (i + 1) + " of " + enemiesPerWave;
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
        isSpawning = false;
    }
}
