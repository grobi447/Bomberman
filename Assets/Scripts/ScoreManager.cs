using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class ScoreManager
{
    public static int p1ScoreValue = 0;
    public static int p2ScoreValue = 0;
    public static int p3ScoreValue = 0;
    public static int roundNumber = 1;


    public static void Reset()
    {
        p1ScoreValue = 0;
        p2ScoreValue = 0;
        p3ScoreValue = 0;
        roundNumber = 1;
    }

    public static void AddPointToP1()
    {
        p1ScoreValue++;
    }

    public static void AddPointToP2()
    {
        p2ScoreValue++;
    }

    public static void AddPointToP3()
    {
        p3ScoreValue++;
    }
}