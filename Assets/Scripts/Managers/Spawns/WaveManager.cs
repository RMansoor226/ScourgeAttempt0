using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int currentWave = 0;
    
    [SerializeField]
    private float waveDelay = 15f;
    [SerializeField]
    private int initialWaveZombies = 2;
    [SerializeField] 
    private int hordeScaleFactor = 2;
    [SerializeField]
    private float baseZombieHealth = 25f;
    [SerializeField]
    private float healthScaleFactor = 1f;
    [SerializeField]
    private float baseZombieSpeed = 2f;
    [SerializeField]
    private float speedScaleFactor = 0.1f;

    [SerializeField] private ZombieSpawner zombieSpawner;

    [SerializeField]
    private RoundCounter roundCounter;

    public Action OnRoundStart;
    public Action OnRoundEnd;
    
    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    private WaveSettings GetWaveSettings(int wave)
    {
        int zombieCount = (initialWaveZombies + (currentWave * hordeScaleFactor));
        float zombieHealth = baseZombieHealth * (currentWave + 1) * healthScaleFactor;
        float zombieSpeed = baseZombieSpeed + (currentWave * speedScaleFactor);

        // Debug.Log(zombieCount + " zombies");
        // Debug.Log(zombieHealth + " health per zombie");
        // Debug.Log(zombieSpeed + " units of speed");
        
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

        // Debug.Log("Starting Wave " + currentWave);
        roundCounter.UpdateRoundCounter(currentWave);

        WaveSettings settings = GetWaveSettings(currentWave);
        
        currentWave++;

        StartCoroutine(zombieSpawner.SpawnWave(settings));
        
        //Debug.Log(zombieCount + " zombies spawned");
        
        yield return new WaitUntil(
            () => ZombieSpawner.Instance.WaveComplete
        );
        
        // Play Round End
        OnRoundEnd?.Invoke();
        
        //Debug.Log("Next wave started");
        StartCoroutine(StartNextWave());
    }
}
