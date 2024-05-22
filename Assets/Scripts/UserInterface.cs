using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class UserInterface : MonoBehaviour
{
    AudioManager audioManager;
    public TMP_Text players;
    private string toDisable;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioManager.PlaySFX(audioManager.click);
        }
    }

    public void LoadMap1()
    {
        ScoreManager.Reset();
        Time.timeScale = 1f;
        toDisable = players.GetComponent<TMP_Text>().text;
        SceneManager.LoadScene("Map1");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void LoadMap2()
    {
        ScoreManager.Reset();
        Time.timeScale = 1f;
        toDisable = players.GetComponent<TMP_Text>().text;
        SceneManager.LoadScene("Map2");
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    public void LoadMap3()
    {
        Time.timeScale = 1f;
        ScoreManager.Reset();
        toDisable = players.GetComponent<TMP_Text>().text;
        SceneManager.LoadScene("Map3");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
       public void LoadMap4()
    {
        Time.timeScale = 1f;
        ScoreManager.Reset();
        toDisable = players.GetComponent<TMP_Text>().text;
        SceneManager.LoadScene("Battle Royale");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (toDisable == "2 Players")
        {
            foreach (GameObject player in players)
            {
                if (player.name == "Player 3")
                {
                    player.SetActive(false);
                }
            }
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ButtonDisable(Button button)
    {
        button.interactable = false;
    }

    public void ButtonEnable(Button button)
    {
        button.interactable = true;
    }
}
