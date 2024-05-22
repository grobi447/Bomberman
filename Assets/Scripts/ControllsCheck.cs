using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllsCheck : MonoBehaviour
{
    public CustomControlls.Controlls Player1;
    public CustomControlls.Controlls Player2;
    public CustomControlls.Controlls Player3;

    public List<string> Player1String = new List<string>();
    public List<string> Player2String = new List<string>();
    public List<string> Player3String = new List<string>();
    public List<string> temp = new List<string>();

    public List<string> forbidden = new List<string>() { "Escape" };

    private string path;
    private string jsonString;

    internal bool CheckDuplicate(CustomControlls.Controlls newControlls, int playerId)
    {
        LoadControlls();

        SetStrings(newControlls, temp);

        foreach (string key in temp)
        {
            //tiltott key-k
            if (0 < CheckInBlock(forbidden, key))
            {
                SetZero();
                return true;
            }

            //önmaga
            if (1 < CheckInBlock(temp, key))
            {
                SetZero();
                return true;
            }

            //többi player
            if (playerId == 1)
            {
                if (0 < CheckInBlock(Player2String, key))
                {
                    SetZero();
                    return true;
                }
                if (0 < CheckInBlock(Player3String, key))
                {
                    SetZero();
                    return true;
                }
            }

            if (playerId == 2)
            {
                if (0 < CheckInBlock(Player1String, key))
                {
                    SetZero();
                    return true;
                }
                if (0 < CheckInBlock(Player3String, key))
                {
                    SetZero();
                    return true;
                }
            }

            if (playerId == 3)
            {
                if (0 < CheckInBlock(Player1String, key))
                {
                    SetZero();
                    return true;
                }
                if (0 < CheckInBlock(Player2String, key))
                {
                    SetZero();
                    return true;
                }
            }
        }

        SetZero();
        return false;
    }

    public void SetZero()
    {
        Player1String.Clear();
        Player2String.Clear();
        Player3String.Clear();
    }

    public void LoadControlls()
    {

        path = Application.dataPath + "/Data/PlayerOneControlls.json";
        jsonString = File.ReadAllText(path);
        Player1 = JsonUtility.FromJson<CustomControlls.Controlls>(jsonString);
        SetStrings(Player1, Player1String);

        path = Application.dataPath + "/Data/PlayerTwoControlls.json";
        jsonString = File.ReadAllText(path);
        Player2 = JsonUtility.FromJson<CustomControlls.Controlls>(jsonString);
        SetStrings(Player2, Player2String);

        path = Application.dataPath + "/Data/PlayerThreeControlls.json";
        jsonString = File.ReadAllText(path);
        Player3 = JsonUtility.FromJson<CustomControlls.Controlls>(jsonString);
        SetStrings(Player3, Player3String);

    }

    //internal
    internal void SetStrings(CustomControlls.Controlls setControll, List<string> PlayerString)
    {
        PlayerString.Add(setControll.up);
        PlayerString.Add(setControll.down);
        PlayerString.Add(setControll.left);
        PlayerString.Add(setControll.right);
        PlayerString.Add(setControll.bomb);
        PlayerString.Add(setControll.barrier);
    }

    public int CheckInBlock(List<string> Block, string key)
    {
        int dupes = 0;

        foreach(string newKey in Block)
        {
            if(newKey == key)
                dupes++;
        }

        return dupes;
    }

}
