using TMPro;
using UnityEngine;

public class RoundCounter : MonoBehaviour
{
    private TMP_Text roundText;

    private void Awake()
    {
        roundText = GetComponent<TMP_Text>();
    }

    public void UpdateRoundCounter(int round)
    {
        Debug.Log("Updating round");
        roundText.text = "Round " + (round + 1);
    }
}
