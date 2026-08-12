using UnityEngine;

public class CombatUI : MonoBehaviour
{
    private bool _combatUiVisible;

    private void Awake()
    {
        _combatUiVisible = true;
    }

    public void SetCombatUi(bool isVisible)
    {
        _combatUiVisible = isVisible;
        gameObject.SetActive(_combatUiVisible);
    }
}
