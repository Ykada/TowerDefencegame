using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Spawnpoint : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject normalEnemy;
    [SerializeField] private GameObject fastBoyWithLowHealth;
    [SerializeField] private GameObject normalBoss;
    [SerializeField] private GameObject fastBoy;
    [SerializeField] private GameObject finalBoss;

    [Header("Dependencies")]
    [SerializeField] private HouseHealth houseHealth;
    [SerializeField] private TowerSpawner towerSpawner;
    [SerializeField] private TimeHandler timeHandler;

    [Header("Wave Settings")]
    [SerializeField] private float spawnInterval = 0.5f;
    [SerializeField] private int totalWaves = 25;
   // [SerializeField]
    private int currentWave = 0;
    private int enemiesRemainingInWave = 0;

    [Header("UI Elements")]
    [SerializeField] private Text waveText;
    //[SerializeField] private Text enemiesLeftText;

    [Header("Spawning Settings")]
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        currentWave++;
        StartCoroutine(StartWave(currentWave ));
       //currentWave = 0;
    }

    private void Start()
    {

    }
    private IEnumerator StartWave(int waveNumber)
    {
        Debug.Log("Starting wave " + waveNumber);
        timeHandler.BroadcastMessage("Currentwave", waveNumber);
        waveText.text = $"Wave: {waveNumber}/{totalWaves}";

        WaveData wave = GetWaveData(waveNumber);
        enemiesRemainingInWave = wave.TotalEnemies();

        yield return StartCoroutine(SpawnEnemies(normalEnemy, wave.normalEnemies));
        yield return StartCoroutine(SpawnEnemies(fastBoyWithLowHealth, wave.fastBoysLowHealth));
        yield return StartCoroutine(SpawnEnemies(normalBoss, wave.normalBosses));
        yield return StartCoroutine(SpawnEnemies(fastBoy, wave.fastBoys));
        yield return StartCoroutine(SpawnEnemies(finalBoss, wave.finalBosses));
    }

    private IEnumerator SpawnEnemies(GameObject enemyPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void EnemyDiedToTower(int moneyEarned)
    {
        towerSpawner.GetComponent<TowerSpawner>().AddMoney(75);
        EnemyDefeated();
    }

    public void EnemyDeathByHouse()
    {
        EnemyDefeated();
        if (houseHealth != null)
        {
            //houseHealth.TakeDamage();
        }
    }

    private void EnemyDefeated()
    {
        enemiesRemainingInWave--;

        if (enemiesRemainingInWave <= 0)//1
        {
            currentWave++;//2
            if (currentWave > totalWaves)
            {
                Debug.Log("All waves completed! You win!");
            }
            else
            {
                StartCoroutine(StartWave(currentWave));
                towerSpawner.GetComponent<TowerSpawner>().AddMoney(250);
            }
        }
    }

    private WaveData GetWaveData(int level)
    {
        WaveData wave = new WaveData();

        switch (level)
        {
            case 1: wave.normalEnemies = 4; break;
            case 2: wave.normalEnemies = 5; break;
            case 3: wave.normalEnemies = 6; wave.fastBoysLowHealth = 2; break;
            case 4: wave.normalEnemies = 7; wave.fastBoysLowHealth = 3; break;
            case 5: wave.normalEnemies = 8; wave.fastBoysLowHealth = 4; wave.normalBosses = 1; break;
            case 6: wave.normalEnemies = 9; wave.fastBoysLowHealth = 5; wave.normalBosses = 2; break;
            case 7: wave.normalEnemies = 14; wave.fastBoysLowHealth = 10; wave.normalBosses = 2; break;
            case 8: wave.normalEnemies = 8; wave.fastBoysLowHealth = 5; wave.normalBosses = 4; break;
            case 9: wave.normalEnemies = 12; wave.fastBoysLowHealth = 10; wave.normalBosses = 5; break;
            case 10: wave.normalEnemies = 20; wave.fastBoysLowHealth = 14; wave.normalBosses = 6; wave.fastBoys = 1; break;
            case 11: wave.normalEnemies = 14; wave.fastBoysLowHealth = 10; wave.normalBosses = 7; wave.fastBoys = 4; break;
            case 12: wave.normalEnemies = 15; wave.fastBoysLowHealth = 11; wave.normalBosses = 8; wave.fastBoys = 5; break;
            case 13: wave.normalEnemies = 16; wave.fastBoysLowHealth = 12; wave.normalBosses = 9; wave.fastBoys = 6; break;
            case 14: wave.normalEnemies = 17; wave.fastBoysLowHealth = 13; wave.normalBosses = 10; wave.fastBoys = 7; break;
            case 15: wave.normalEnemies = 18; wave.fastBoysLowHealth = 14; wave.normalBosses = 11; wave.fastBoys = 8; break;
            case 16: wave.normalEnemies = 19; wave.fastBoysLowHealth = 15; wave.normalBosses = 12; wave.fastBoys = 9; break;
            case 17: wave.normalEnemies = 20; wave.fastBoysLowHealth = 16; wave.normalBosses = 13; wave.fastBoys = 10; break;
            case 18: wave.normalEnemies = 21; wave.fastBoysLowHealth = 17; wave.normalBosses = 14; wave.fastBoys = 11; break;
            case 19: wave.normalEnemies = 22; wave.fastBoysLowHealth = 18; wave.normalBosses = 15; wave.fastBoys = 12; break;
            case 20: wave.normalEnemies = 23; wave.fastBoysLowHealth = 19; wave.normalBosses = 16; wave.fastBoys = 13; break;
            case 21: wave.normalEnemies = 24; wave.fastBoysLowHealth = 20; wave.normalBosses = 17; wave.fastBoys = 14; break;
            case 22: wave.normalEnemies = 25; wave.fastBoysLowHealth = 21; wave.normalBosses = 18; wave.fastBoys = 15; break;
            case 23: wave.normalEnemies = 26; wave.fastBoysLowHealth = 22; wave.normalBosses = 19; wave.fastBoys = 16; break;
            case 24: wave.normalEnemies = 30; wave.fastBoysLowHealth = 25; wave.normalBosses = 25; wave.fastBoys = 20; break;
          
            case 25: wave.finalBosses = 1; break;
        }
        return wave;
    }
    private class WaveData
    {
        public int normalEnemies = 0;
        public int fastBoysLowHealth = 0;
        public int normalBosses = 0;
        public int fastBoys = 0;
        public int finalBosses = 0;

        public int TotalEnemies()
        {
            return normalEnemies + fastBoysLowHealth + normalBosses + fastBoys + finalBosses;
        }
    }
}
