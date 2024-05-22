using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreUI : MonoBehaviour
{
    public TMP_Text p1ScoreText;
    public TMP_Text p2ScoreText;
    public TMP_Text p3ScoreText;
    public TMP_Text roundNumberText;

    void Update()
    {
        UpdateScoreTexts();
        UpdateRoundNumber();
        if (Players.instance.players == 3)
        {
            p3ScoreText.text = "P3: " + ScoreManager.p3ScoreValue.ToString();
            p3ScoreText.gameObject.SetActive(true);
        }
        else
        {
            p3ScoreText.gameObject.SetActive(false);
        }

    }

    void UpdateScoreTexts()
    {
        p1ScoreText.text = "P1: " + ScoreManager.p1ScoreValue.ToString();
        p2ScoreText.text = "P2: " + ScoreManager.p2ScoreValue.ToString();
        p3ScoreText.text = "P3: " + ScoreManager.p3ScoreValue.ToString();
    }

    void UpdateRoundNumber()
    {
        roundNumberText.text = "Round: " + ScoreManager.roundNumber.ToString();
    }

    
}
