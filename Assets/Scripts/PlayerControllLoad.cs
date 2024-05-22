using UnityEngine;
using System.IO;
using System;

public class PlayerControllLoad : MonoBehaviour
{
    //Json file serializalasahoz szukseges
    [System.Serializable]
    //Jatekos controlljai
    public class Controlls
    {
        public string up;
        public string down;
        public string left;
        public string right;
        public string bomb;
        public string barrier;
    }

    public Controlls currentControls;

    public GameObject currentPlayer;

    private string path;
    private string jsonString;


    // adott player controlljait betolti
    private void Awake()
    {
        if (currentPlayer.name == "Player 1")
            path = Application.dataPath + "/Data/PlayerOneControlls.json";
        else if (currentPlayer.name == "Player 2")
            path = Application.dataPath + "/Data/PlayerTwoControlls.json";
        else if (currentPlayer.name == "Player 3")
            path = Application.dataPath + "/Data/PlayerThreeControlls.json";

        jsonString = File.ReadAllText(path);
        currentControls = JsonUtility.FromJson<Controlls>(jsonString);

    }

    // getterek
    public KeyCode GetUp() { return (KeyCode)Enum.Parse(typeof(KeyCode), currentControls.up); }
    public KeyCode GetDown() { return (KeyCode)Enum.Parse(typeof(KeyCode), currentControls.down); }
    public KeyCode GetLeft() { return (KeyCode)Enum.Parse(typeof(KeyCode), currentControls.left); }
    public KeyCode GetRight() { return (KeyCode)Enum.Parse(typeof(KeyCode), currentControls.right); }
    public KeyCode GetBomb() { return (KeyCode)Enum.Parse(typeof(KeyCode), currentControls.bomb); }
    public KeyCode GetBarrier() { return (KeyCode)Enum.Parse(typeof(KeyCode), currentControls.barrier); }
}