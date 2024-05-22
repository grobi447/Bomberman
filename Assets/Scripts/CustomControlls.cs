using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomControlls : MonoBehaviour
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
    public Controlls temp;

    public TMP_Text playerText;
    private string currentPlayer;

    private string path;
    private string jsonString;

    public Button upButton;
    public Button downButton;
    public Button leftButton;
    public Button rightButton;
    public Button bombButton;
    public Button barrierButton;


    //eppen aktualis jatekos iranyitasainak megjelenitese
    private void Awake()
    {
        currentPlayer = playerText.GetComponent<TMP_Text>().text;
        WriteOutControlls();
    }

    //update fuggveny a user jatekosok kozotti valtasara es controll mentesere
    private void Update()
    {
        //jatekosok kozotti valtas
        string newCurrentPlayer = playerText.GetComponent<TMP_Text>().text;
        if (currentPlayer != newCurrentPlayer)
        {
            currentPlayer = newCurrentPlayer;
            WriteOutControlls();
        }

    }

    void OnGUI()
    {
        //controll figyelese
        Event e = Event.current;
        if (e.isKey)
        {
            ButtonTextChange(e.keyCode.ToString());
        }
    }

    public void ButtonTextChange(string newText)
    {
        if(EventSystem.current.currentSelectedGameObject.name != null)
        {
            switch (EventSystem.current.currentSelectedGameObject.name)
            {
                case "UpButton":
                    upButton.GetComponentInChildren<TMP_Text>().text = newText;
                    break;
                case "DownButton":
                    downButton.GetComponentInChildren<TMP_Text>().text = newText;
                    break;
                case "LeftButton":
                    leftButton.GetComponentInChildren<TMP_Text>().text = newText;
                    break;
                case "RightButton":
                    rightButton.GetComponentInChildren<TMP_Text>().text = newText;
                    break;
                case "BombButton":
                    bombButton.GetComponentInChildren<TMP_Text>().text = newText;
                    break;
                case "BarrierButton":
                    barrierButton.GetComponentInChildren<TMP_Text>().text = newText;
                    break;
                default:
                    break;
            }

        }

    }

    public void WriteOutControlls()
    {
        //megfelelo jsonbol beolvas
        JsonPath();
        LoadControlls();

        //controlls alapjan kiirat
        upButton.GetComponentInChildren<TMP_Text>().text = currentControls.up;
        downButton.GetComponentInChildren<TMP_Text>().text = currentControls.down;
        leftButton.GetComponentInChildren<TMP_Text>().text = currentControls.left;
        rightButton.GetComponentInChildren<TMP_Text>().text = currentControls.right;
        bombButton.GetComponentInChildren<TMP_Text>().text = currentControls.bomb;
        barrierButton.GetComponentInChildren<TMP_Text>().text = currentControls.barrier;

    }

    //vissza adja string-kent a json file utvonalat attol fuggoen melyik jatekos beallitasait nezi a user eppen
    public string JsonPath()
    {
        if (currentPlayer == "Player 1")
            path = Application.dataPath + "/Data/PlayerOneControlls.json";
        else if (currentPlayer == "Player 2")
            path = Application.dataPath + "/Data/PlayerTwoControlls.json";
        else if (currentPlayer == "Player 3")
            path = Application.dataPath + "/Data/PlayerThreeControlls.json";
        else path = Application.dataPath + "/Data/PlayerOneControlls.json";

        return path;
    }

    //json file-bol beolvassa az adott jatekos controlljat
    public void LoadControlls()
    {
        jsonString = File.ReadAllText(path);
        currentControls = JsonUtility.FromJson<Controlls>(jsonString);

    }

    //json file-ba irja az adott jatekos controlljat
    public void SaveControlls()
    {
        bool canSave = true;

        temp.up = upButton.GetComponentInChildren<TMP_Text>().text;
        temp.down = downButton.GetComponentInChildren<TMP_Text>().text;
        temp.left = leftButton.GetComponentInChildren<TMP_Text>().text;
        temp.right = rightButton.GetComponentInChildren<TMP_Text>().text;
        temp.bomb = bombButton.GetComponentInChildren<TMP_Text>().text;
        temp.barrier = barrierButton.GetComponentInChildren<TMP_Text>().text;

        if (currentPlayer == "Player 1")
            canSave = !GetComponent<ControllsCheck>().CheckDuplicate(temp, 1);
        else if (currentPlayer == "Player 2")
            canSave = !GetComponent<ControllsCheck>().CheckDuplicate(temp, 2);
        else if (currentPlayer == "Player 3")
            canSave = !GetComponent<ControllsCheck>().CheckDuplicate(temp, 3);

        if (canSave)
        {
            currentControls = temp;
            string newControlls = JsonUtility.ToJson(currentControls);
            File.WriteAllText(path, newControlls);
        }

        WriteOutControlls();

    }

}