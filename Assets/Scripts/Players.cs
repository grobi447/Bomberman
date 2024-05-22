using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Players : MonoBehaviour
{
    public static Players instance;
    public int players = 2;

    public TMP_Text playerText;

    private void Start()
    {
        UpdatePlayerNumber();
    }

    public void IncrementPlayer()
    {
        if (players == 2)
        {
            players++;
        }
        UpdatePlayerNumber();
    }

    public void DecrementPlayer()
    {
        if (players == 3)
        {
            players--;
        }
        UpdatePlayerNumber();
    }
    private void UpdatePlayerNumber()
    {
        playerText.text = players + " Players";
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
