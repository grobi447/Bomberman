using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    public Rounds rounds;
    public Players playernumber;
    string toDisable;
    public TMP_Text winnerText;
    public TMP_Text Player1Score;
    public TMP_Text Player2Score;
    public TMP_Text Player3Score;
    public GameObject winnerPanel;
    public GameObject winscreen;

    private void Start()
    {
        rounds = Rounds.instance;
        playernumber = Players.instance;
        players = GameObject.FindGameObjectsWithTag("Player");
        winnerPanel.SetActive(false);
        winscreen.SetActive(false);
    }
    private void winner()
    {
        Time.timeScale = 0f;
        winnerPanel.SetActive(true);
        winscreen.SetActive(true);
        if (ScoreManager.p1ScoreValue > ScoreManager.p2ScoreValue && ScoreManager.p1ScoreValue > ScoreManager.p3ScoreValue)
        {
            winnerText.text = "Player 1 Wins!";
        }
        else if (ScoreManager.p2ScoreValue > ScoreManager.p1ScoreValue && ScoreManager.p2ScoreValue > ScoreManager.p3ScoreValue)
        {
            winnerText.text = "Player 2 Wins!";
        }
        else if (ScoreManager.p3ScoreValue > ScoreManager.p1ScoreValue && ScoreManager.p3ScoreValue > ScoreManager.p2ScoreValue)
        {
            winnerText.text = "Player 3 Wins!";
        }
        else
        {
            winnerText.text = "Tie!";
        }
        Player1Score.text = "Player 1: " + ScoreManager.p1ScoreValue;
        Player2Score.text = "Player 2: " + ScoreManager.p2ScoreValue;
        if (Players.instance.players == 3)
        {
            Player3Score.text = "Player 3: " + ScoreManager.p3ScoreValue;
        }
    }
    public void CheckGameEnd()
    {
        int alivecount = 0;
        GameObject lastAlivePlayer = null;

        foreach (GameObject player in players)
        {
            if (player.activeSelf)
            {
                alivecount++;
                lastAlivePlayer = player;
            }
        }

        if (alivecount <= 1 && lastAlivePlayer != null)
        {
            StartCoroutine(CheckLastPlayerAlive(lastAlivePlayer));
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator CheckLastPlayerAlive(GameObject player)
    {
        yield return new WaitForSeconds(2.99f);

        if (player.activeSelf)
        {

            AddPointToPlayer(player);
            ScoreManager.roundNumber++;
            if (ScoreManager.roundNumber > rounds.rounds)
            {
                winner();
                //SceneManager.LoadScene("EndGame");
            }
            else
            {
                Invoke(nameof(NewRound), 0f);
            }
        }
        else
        {
            Invoke(nameof(NewRound), 3f);
        }
    }
    private void AddPointToPlayer(GameObject player)
    {
        switch (player.name)
        {
            case "Player 1":
                ScoreManager.AddPointToP1();
                break;
            case "Player 2":
                ScoreManager.AddPointToP2();
                break;
            case "Player 3":
                ScoreManager.AddPointToP3();
                break;
        }
    }

    public void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.sceneLoaded += NewRoundPlayerLimiter;
    }

    public void NewRoundPlayerLimiter(Scene scene, LoadSceneMode mode)
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (playernumber.players == 2)
        {
            foreach (GameObject player in players)
            {
                if (player.name == "Player 3")
                {
                    player.SetActive(false);
                }
            }
        }
        SceneManager.sceneLoaded -= NewRoundPlayerLimiter;
    }
}