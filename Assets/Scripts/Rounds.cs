using System.Collections;
using TMPro;
using UnityEngine;

public class Rounds : MonoBehaviour
{
    public static Rounds instance;
    public int rounds = 3;
    public TMP_Text roundText;

    private void Start()
    {
        UpdateRoundNumber();
    }

    private void Update()
    {
        UpdateRoundNumber();
    }

    public void IncrementRound()
    {
        rounds++;
        UpdateRoundNumber();
    }

    public void DecrementRound()
    {
        if (rounds > 1)
        {
            rounds--;
            UpdateRoundNumber();
        }
    }

    public void UpdateRoundNumber()
    {
        if (roundText != null)
        {
            roundText.text = "Round: " + rounds;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
