using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class RoundsTest
{
    private GameObject roundsObject;
    private Rounds roundsInstance;
    private TMP_Text roundText;

    [SetUp]
    public void Setup()
    {
        roundsObject = new GameObject();
        roundsInstance = roundsObject.AddComponent<Rounds>();
        roundText = roundsObject.AddComponent<TextMeshProUGUI>();
        roundsInstance.roundText = roundText;
    }

    [UnityTest]
    public IEnumerator TestIncrementRound()
    {
        int initialRounds = roundsInstance.rounds;
        roundsInstance.IncrementRound();
        Assert.AreEqual(initialRounds + 1, roundsInstance.rounds);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestDecrementRound()
    {
        roundsInstance.rounds = 3;
        roundsInstance.DecrementRound();
        Assert.AreEqual(2, roundsInstance.rounds);
        yield return null;
    }
}
