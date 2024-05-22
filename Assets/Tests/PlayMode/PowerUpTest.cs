using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PowerUpTest
{
  private BombController _bombController;

  [SetUp]
  public void SetUp()
  {
    _bombController = new GameObject().AddComponent<BombController>();
  }

  [UnityTest]
  public IEnumerator Invincible()
  {
    var model = new MovementControllerModel();
    var initialinvincibility = model.invincible;

    model.Invincible();
    
    Assert.AreEqual(true, model.invincible);

    yield return new WaitForSeconds(7);

    Assert.AreEqual(false, model.invincible);
    yield return null;
  }

  [UnityTest]
  public IEnumerator Ghost()
  {
    var model = new MovementControllerModel();
    var initialinvincibility = model.ghost;

    model.Ghost();
    
    Assert.AreEqual(true, model.ghost);

    yield return new WaitForSeconds(5);

    Assert.AreEqual(false, model.ghost);
    yield return null;
  }

  [UnityTest]
  public IEnumerator PlaceAllBombs()
  {
    var initialBombsRemaining = _bombController.bombsRemaining;
    _bombController.bombsRemaining = 0;
    _bombController.PlaceAllBombs();
    Assert.IsTrue(_bombController.placebomb);

    yield return new WaitForSeconds(6);
    Assert.IsFalse(_bombController.placebomb);
    _bombController.bombsRemaining = initialBombsRemaining;
    yield return null;
  }
}
