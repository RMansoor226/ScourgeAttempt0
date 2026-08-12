using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    private bool _deathScreenVisible;

    private void Awake()
    {
        _deathScreenVisible = false;
    }

    public void SetDeathScreen(bool isVisible)
    {
        _deathScreenVisible = isVisible;
        gameObject.SetActive(_deathScreenVisible);
    }
}
