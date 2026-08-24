using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombiePrefab;
    [SerializeField] 
    private ZombiePool zombiePool;
    [SerializeField] 
    private Transform[] spawnPoints;
    [SerializeField]
    private float spawnDelay = 3f;
    [SerializeField]
    private int maxZombies = 6;

    private int _zombiesRemaining;
    private int _zombiesAlive;

    private int _waveSpawnID;

    public bool WaveComplete => 
        _zombiesRemaining == 0 && 
        _zombiesAlive == 0;
    
    public static ZombieSpawner Instance { get; private set; }
    
    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (zombiePrefab == null)
        {
            Debug.LogError("Zombie Prefab isn't instantiated");
        }
        
        if (zombiePool == null)
        {
            Debug.LogError("Zombie Pool isn't instantiated");
        }
        
        if (spawnPoints.Length <= 0)
        {
            Debug.LogError("No spawn points were instantiated");
        }

        _zombiesAlive = 0;
    }
    
    private void SpawnZombie(WaveSettings settings)
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomSpawnIndex];
        
        // Retrieve zombie from pool
        Zombie zombie = zombiePool.GetZombie();
        
        if (zombie == null)
        {
            Debug.LogError("Failed to retrieve zombie from pool!");
            return;
        }
        
        zombie.transform.position = spawnPoint.position;
        
        // Initialize zombie stats
        zombie.gameObject.SetActive(true);
        zombie.Initialize(settings);

        // Subscribe to OnDeath and remove existing subscriptions
        Health health = zombie.GetZombieHealth();
        
        health.OnDeath -= ZombieDied;
        health.OnDeath += ZombieDied;
    }

    public void ZombieDied()
    {
        if (_zombiesAlive <= 0)
        {
            Debug.LogError("ZombieDied called when no zombies were alive.");
            return;
        }
        
        _zombiesAlive--;
        
        Debug.Log($"Zombie just died. Now {_zombiesRemaining} Remaining. {_zombiesAlive} Alive.");
    }
    
    public IEnumerator SpawnWave(WaveSettings settings)
    {
        int spawnID = ++_waveSpawnID;
        
        Debug.Log($"STARTING SPAWN WAVE COROUTINE {spawnID}");
        
        _zombiesRemaining = settings.zombieCount;
        
        while (_zombiesRemaining > 0)
        {
            if (_zombiesAlive < maxZombies)
            {
                SpawnZombie(settings);
                _zombiesAlive++;
                _zombiesRemaining--;
            }
            
            Debug.Log(
                $"Wave coroutine {spawnID}: " +
                $"{_zombiesRemaining} Remaining, " +
                $"{_zombiesAlive} Alive."
            );
            
            yield return new WaitForSeconds(spawnDelay);
        }
        
        Debug.Log($"ENDING SPAWN WAVE COROUTINE {spawnID}");
    }
}
