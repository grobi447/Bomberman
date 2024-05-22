using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTest
{
    [SetUp]
    public void Setup()
    {
        ScoreManager.Reset();
    }

    [UnityTest]
    public IEnumerator TestAddPoint()
    {
        int initP1Score = ScoreManager.p1ScoreValue;
        ScoreManager.AddPointToP1();
        Assert.AreEqual(initP1Score + 1, ScoreManager.p1ScoreValue);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestAddPoint2()
    {
        int initP2Score = ScoreManager.p2ScoreValue;
        ScoreManager.AddPointToP2();
        Assert.AreEqual(initP2Score + 1, ScoreManager.p2ScoreValue);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestAddPoint3()
    {
        int initP3Score = ScoreManager.p3ScoreValue;
        ScoreManager.AddPointToP3();
        Assert.AreEqual(initP3Score + 1, ScoreManager.p3ScoreValue);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestIncreaseSpeedIfHasSpeedboost()
    {
        var model = new MovementControllerModel();
        var initialSpeed = model.moveSpeed;
        model.speedboosted = true;
        model.IncreaseSpeed();
        Assert.AreEqual(initialSpeed, model.moveSpeed);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestIncreaseSpeedIfHasNoSpeedboost()
    {
        var model = new MovementControllerModel();
        var initialSpeed = model.moveSpeed;
        model.speedboosted = false;
        model.IncreaseSpeed();
        Assert.AreEqual(initialSpeed + 1.0f, model.moveSpeed);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestLowSpeedIfSpeedboosted()
    {
        var model = new MovementControllerModel();
        model.speedboosted = true;
        model.LowSpeed();
        Assert.AreEqual(3.0f, model.moveSpeed);
        yield return new WaitForSeconds(6);
        Assert.AreEqual(6.0f, model.moveSpeed);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TestLowSpeedIfNotSpeedboosted()
    {
        var model = new MovementControllerModel();
        model.speedboosted = false;
        model.LowSpeed();
        Assert.AreEqual(3.0f, model.moveSpeed);
        yield return new WaitForSeconds(6);
        Assert.AreEqual(5.0f, model.moveSpeed);
        yield return null;
    }
}
