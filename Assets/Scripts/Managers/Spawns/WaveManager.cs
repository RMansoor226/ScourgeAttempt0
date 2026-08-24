using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 0;

    [SerializeField] 
    private bool spawnZombies = true;
    [SerializeField]
    private float waveDelay = 15f;
    [SerializeField]
    private int initialWaveZombies = 6;
    [SerializeField] 
    private int hordeScaleFactor = 2;
    [SerializeField]
    private float baseZombieHealth = 25f;
    [SerializeField]
    private float healthScaleFactor = 1f;
    [SerializeField]
    private float baseZombieSpeed = 2f;
    [SerializeField]
    private float speedScaleFactor = 0.25f;

    [SerializeField] 
    private ZombieSpawner zombieSpawner;
    [SerializeField]
    private RoundCounter roundCounter;

    public Action OnRoundStart;
    public Action OnRoundEnd;
    
    private void Start()
    {
        if (spawnZombies)
        {
            StartCoroutine(StartNextWave());
        }
        else
        {
            Debug.Log("Zombies are not spawning!");
        }
    }

    private WaveSettings GetWaveSettings(int wave)
    {
        int zombieCount = (initialWaveZombies + (currentWave * hordeScaleFactor));
        float zombieHealth = baseZombieHealth * (currentWave + 1) * healthScaleFactor;
        float zombieSpeed = baseZombieSpeed + (currentWave * speedScaleFactor);
        
        return new WaveSettings(
            zombieCount,
            zombieHealth,
            zombieSpeed
            );
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(waveDelay);
        
        // Play Round Start Sound
        OnRoundStart?.Invoke();

        roundCounter.UpdateRoundCounter(currentWave);

        WaveSettings settings = GetWaveSettings(currentWave);
        
        currentWave++;

        StartCoroutine(zombieSpawner.SpawnWave(settings));
        
        yield return new WaitUntil(
            () => ZombieSpawner.Instance.WaveComplete
        );
        
        // Play Round End
        OnRoundEnd?.Invoke();
        Debug.Log("Ending round!");
        
        StartCoroutine(StartNextWave());
    }
}
