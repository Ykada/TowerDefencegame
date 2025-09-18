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
        waveCountdownText.text = "current enemies" + enemiesPerWave;

    }
    void startWave()
    {
        isSpawning = true;
        currentWave++;
        enemiesPerWave ++;
        waveTimer = timeBetweenWaves;
        SpawnEnemies();
    }

    void startnextwave()
    {
        if (enemiesAlive < 1)
        {
            enemiesPerWave++;
            currentWave++;
            SpawnEnemies();
        }
    }

    public void ennemydeath()
    {

        enemiesAlive--;
        towerSpawner.GetComponent<TowerSpawner>().ennemydeath1(15);
        startnextwave();

    }
    void SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            Transform spawnPoint = spawnLocations[Random.Range(0, spawnLocations.Length)];
            GameObject enemyPrefab = ennemysToSpawn[Random.Range(0, ennemysToSpawn.Length)];
            waveCountdownText.text = "Spawning Enemy " + (i + 1) + " of " + enemiesPerWave;
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            enemiesAlive++;
            towerSpawner.GetComponent<TowerSpawner>().ennemydeath1(100);
        }
        isSpawning = false;
    }
}
