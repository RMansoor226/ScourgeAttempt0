using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 0;
    public int initialWaveZombies = 2;
    public float waveDelay = 15f;

    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(waveDelay);

        Debug.Log("Starting Wave " + currentWave);
        
        int zombieCount = initialWaveZombies + (currentWave * 2);
        currentWave++;

        ZombieSpawner.Instance.StartCoroutine(ZombieSpawner.Instance.SpawnWave(zombieCount));
        
        Debug.Log(zombieCount + " zombies spawned");
        
        yield return new WaitUntil(
            () => ZombieSpawner.Instance.WaveComplete
        );
        
        Debug.Log("Next wave started");
        StartCoroutine(StartNextWave());
    }
}
