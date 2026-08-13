using UnityEngine;

public class WaveSettings
{
    public int zombieCount { get; private set; }
    public float zombieHealth { get; private set; }
    public float zombieSpeed { get; private set; }

    public WaveSettings(
        int zombieCount, 
        float zombieHealth, 
        float zombieSpeed)
    {
        this.zombieCount = zombieCount;
        this.zombieHealth = zombieHealth;
        this.zombieSpeed = zombieSpeed;
    }
    
}
