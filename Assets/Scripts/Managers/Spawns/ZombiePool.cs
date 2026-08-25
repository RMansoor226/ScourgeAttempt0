using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombiePool : MonoBehaviour
{
    private Queue<Zombie> _zombiePool;

    [SerializeField] 
    private GameObject zombiePrefab;
    [SerializeField] 
    private Transform poolSpawnPoint;
    [SerializeField] 
    private int maxZombies = 10;

    private int _poolSize;

    private void Awake()
    {
        _poolSize = maxZombies;
        _zombiePool = new Queue<Zombie>();
        InitializeZombiePool();
    }

    private void InitializeZombiePool()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject zombieObject = Instantiate(
                zombiePrefab,
                poolSpawnPoint.position,
                poolSpawnPoint.rotation
            );
            Zombie zombie = zombieObject.GetComponent<Zombie>();
            
            zombie.SetZombiePool(this);
            zombieObject.SetActive(false);
            
            _zombiePool.Enqueue(zombie);
        }
    }

    public Zombie GetZombie()
    {
        // Debug.Log("Zombie pool count is " + _zombiePool.Count);
        if (_zombiePool.Count == 0)
        {
            Debug.LogError("No more zombies left in pool!");
            return null;
        }
        
        return _zombiePool.Dequeue();
    }

    public void ReturnZombie(Zombie zombie)
    {
        // Debug.Log($"Returning zombie: {zombie.gameObject.name}");
        
        zombie.Reset();
        zombie.gameObject.transform.position = poolSpawnPoint.position;
        zombie.gameObject.SetActive(false);
        
        _zombiePool.Enqueue(zombie);
    }

    public bool IsPoolEmpty()
    {
        return _zombiePool.Count == 0;
    }

    public int GetMaxZombies()
    {
        return maxZombies;
    }
}
