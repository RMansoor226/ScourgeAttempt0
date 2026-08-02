using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnSettings spawnSettings;
    
    [SerializeField]
    private GameObject zombiePrefab;

    [SerializeField] 
    private Transform[] spawnPoints;

    public int zombiesAlive = 0;
    private int _spawnedZombies = 0;

    private void Awake()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void Start()
    {
        SpawnZombie();
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

        ZombieAI zombieAI = zombie.GetComponent<ZombieAI>();
        zombieAI.Initialize(this);
        
        zombiesAlive++;
        _spawnedZombies++;
        Debug.Log("Spawning zombie on spawnPoint " + randomSpawnIndex);
    }

    public void ZombieDied()
    {
        zombiesAlive--;
    }
    
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (zombiesAlive < spawnSettings.maxZombiesAlive  && _spawnedZombies < spawnSettings.maxZombiesPerRound)
            {
                SpawnZombie();
            }
        
            yield return new WaitForSeconds(spawnSettings.spawnDelay);
        }
    }
}
