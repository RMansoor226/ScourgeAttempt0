using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    private bool _deathScreenVisible;

    private void Awake()
    {
        _deathScreenVisible = false;
    }

    public void ToggleDeathScreen()
    {
        _deathScreenVisible = !_deathScreenVisible;
        gameObject.SetActive(_deathScreenVisible);
    }
}
