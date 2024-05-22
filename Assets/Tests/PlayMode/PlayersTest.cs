using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using System.Collections;

public class PlayersTests
{
    private GameObject playersGameObject;
    private Players players;

    [SetUp]
    public void Setup()
    {
        playersGameObject = new GameObject();
        players = playersGameObject.AddComponent<Players>();

        var textObject = new GameObject();
        var tmpText = textObject.AddComponent<TextMeshProUGUI>();
        players.playerText = tmpText;

        players.Invoke("Awake", 0);
        players.Invoke("Start", 0);
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(playersGameObject);
        Object.DestroyImmediate(players.playerText.gameObject);
    }

    [UnityTest]
    public IEnumerator InitialPlayerCountIsTwo()
    {
        yield return null;
        Assert.AreEqual(2, players.players);
    }

    [UnityTest]
    public IEnumerator IncrementPlayerCount()
    {
        players.IncrementPlayer();
        yield return null;
        Assert.AreEqual(3, players.players);
        Assert.AreEqual("3 Players", players.playerText.text);
    }

    [UnityTest]
    public IEnumerator IncrementPlayerCountDoesNotExceedThree()
    {
        players.IncrementPlayer();
        players.IncrementPlayer();
        yield return null;
        Assert.AreEqual(3, players.players);
        Assert.AreEqual("3 Players", players.playerText.text);
    }

    [UnityTest]
    public IEnumerator DecrementPlayerCount()
    {
        players.IncrementPlayer();
        players.DecrementPlayer();
        yield return null;
        Assert.AreEqual(2, players.players);
        Assert.AreEqual("2 Players", players.playerText.text);
    }

    [UnityTest]
    public IEnumerator DecrementPlayerCountDoesNotGoBelowTwo()
    {
        players.DecrementPlayer();
        yield return null;
        Assert.AreEqual(2, players.players);
        Assert.AreEqual("2 Players", players.playerText.text);
    }
}
