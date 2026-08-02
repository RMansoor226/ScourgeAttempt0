using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject zombiePrefab;

    [SerializeField] 
    private Transform[] spawnPoints;

    private int _zombiesRemaining;
    private int _zombiesAlive;
    private readonly float _spawnDelay = 3f;
    private readonly int _maxZombies = 4;

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
    
    private void SpawnZombie()
    {
        int randomSpawnIndex = Random.Range(0, spawnPoints.Length);

        Transform spawnPoint = spawnPoints[randomSpawnIndex];
        
        GameObject zombie = Instantiate(
            zombiePrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        zombie.GetComponent<Health>().OnDeath += ZombieDied;

        ZombieAI zombieAI = zombie.GetComponent<ZombieAI>();
        zombieAI.Initialize(this);
        
        _zombiesAlive++;
        //Debug.Log("Spawning zombie on spawnPoint " + randomSpawnIndex);
    }

    public void ZombieDied()
    {
        _zombiesAlive--;
        Debug.Log(_zombiesAlive + " zombies alive");
    }
    
    public IEnumerator SpawnWave(int zombiesPerWave)
    {
        _zombiesRemaining = zombiesPerWave;
        Debug.Log("Max zombies is: " + zombiesPerWave);
        
        while (_zombiesRemaining > 0)
        {
            if (_zombiesAlive < _maxZombies)
            {
                SpawnZombie();
                _zombiesRemaining--;
                Debug.Log(_zombiesRemaining + " zombies remaining");
            }
            
            yield return new WaitForSeconds(_spawnDelay);
        }
    }
}
