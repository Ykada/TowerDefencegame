using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Spawnpoint : MonoBehaviour
{
    #region

    [SerializeField] private GameObject[] ennemysToSpawn;
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private int waveNumber = 1;
    [SerializeField] private float timeBetweenWaves = 25f;
    public TowerSpawner towerSpawner;

    #endregion

    #region

    public Text waveCountdownText;
    public Text waveNumberText;

    #endregion

    #region

    private float countdown = 2f;
    private int enemiesAlive;
    private bool isSpawning = false;
    [SerializeField] private int currentWave;
    private float waveTimer = 25f;
    [SerializeField] private int enemiesPerWave;
    [SerializeField] private int maxWaves = 25;

    #endregion

    private void Start()
    {
        startWave();
    }

    private void Update()
    {
        waveNumberText.text = "Wave: " + currentWave + "/" + maxWaves;
        waveCountdownText.text = "Current Enemies: " + enemiesAlive;
    }

    void startWave()
    {
        isSpawning = true;
        currentWave++;
        enemiesPerWave++;
        waveTimer = timeBetweenWaves;
        SpawnEnemies();
    }

    void startnextwave()
    {
        if (enemiesAlive < 1 && currentWave < maxWaves)
        {
            enemiesPerWave++;
            currentWave++;
            SpawnEnemies();
        }
    }

    public void ennemydeath()
    {
        enemiesAlive--;
        towerSpawner.GetComponent<TowerSpawner>().ennemydeath1(65);
        startnextwave();
    }
    public void ennemydeathbyhouse()
    {
        enemiesAlive--;
        startnextwave();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawnPoint = spawnLocations[Random.Range(0, spawnLocations.Length)];
            GameObject enemyPrefab = GetEnemyForWave(currentWave, i);
            waveCountdownText.text = "Spawning Enemy " + (i + 1) + " of " + enemiesPerWave;
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemiesAlive++;
        }
        isSpawning = false;
    }

    #region wave Logic
    GameObject GetEnemyForWave(int wave, int index)
    {
        // 0–3 = normal enemies
        // 4 = boss
        // 5 = faster boss
        // 6 = final boss

        if (wave >= 1 && wave <= 4)
        {
            return ennemysToSpawn[Random.Range(0, 4)];
        }
        else if (wave >= 5 && wave <= 6)
        {
            if (index == 0) return ennemysToSpawn[8];
            return ennemysToSpawn[Random.Range(0, 4)];
        }
        else if (wave >= 7 && wave <= 9)
        {
            if (Random.value < 0.25f) return ennemysToSpawn[16];
            return ennemysToSpawn[Random.Range(0, 4)];
        }
        else if (wave >= 10 && wave <= 18)
        {
            return ennemysToSpawn[Random.Range(4, 6)];
        }
        else if (wave >= 19 && wave <= 24)
        {
            return ennemysToSpawn[Random.Range(0, ennemysToSpawn.Length - 1)];
        }
        else if (wave == 25)
        {
            return ennemysToSpawn[35];
        }

        return ennemysToSpawn[0];
        #endregion
    }
}
