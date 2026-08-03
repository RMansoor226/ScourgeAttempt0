using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombiePrefab;

    [SerializeField] 
    private Transform[] spawnPoints;
    
    [SerializeField]
    private float spawnDelay = 3f;
    
    [SerializeField]
    private int maxZombies = 4;

    private int _zombiesRemaining;
    private int _zombiesAlive;

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

        _zombiesAlive = 0;
    }
    
    private void SpawnZombie(WaveSettings settings)
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);

        Transform spawnPoint = spawnPoints[randomSpawnIndex];
        
        GameObject zombieObject = Instantiate(
            zombiePrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );
        
        // Initialize zombie difficulty stats
        Zombie zombie = zombieObject.GetComponent<Zombie>();
        zombie.Initialize(settings);

        zombie.GetZombieHealth().OnDeath += ZombieDied;
        
        _zombiesAlive++;
        //Debug.Log("Spawning zombie on spawnPoint " + randomSpawnIndex);
    }

    public void ZombieDied()
    {
        _zombiesAlive--;
        Debug.Log(_zombiesAlive + " zombies alive");
    }
    
    public IEnumerator SpawnWave(WaveSettings settings)
    {
        _zombiesRemaining = settings.zombieCount;
        //Debug.Log("Max zombies is: " + zombiesPerWave);
        
        while (_zombiesRemaining > 0)
        {
            if (_zombiesAlive < maxZombies)
            {
                SpawnZombie(settings);
                _zombiesRemaining--;
                //Debug.Log(_zombiesRemaining + " zombies remaining");
            }
            
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
