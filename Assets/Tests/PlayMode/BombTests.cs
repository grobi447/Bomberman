using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BombControllerTests
{
  private BombController _bombController;

  [SetUp]
  public void SetUp()
  {
    _bombController = new GameObject().AddComponent<BombController>();
  }

  [UnityTest]
  public IEnumerator IncreasesBombCount()
  {
    var initialBombCount = _bombController.bombsRemaining;
    var initialMaxBombs = _bombController.maxBombs;

    _bombController.AddBomb();

    Assert.AreEqual(initialBombCount + 1, _bombController.bombsRemaining);
    Assert.AreEqual(initialMaxBombs + 1, _bombController.maxBombs);
    yield return null;
  }

  [UnityTest]
  public IEnumerator IncreasesRange()
  {
    var initialRange = _bombController.explosionRadius;

    _bombController.IncreaseRadius();

    Assert.AreEqual(initialRange + 1, _bombController.explosionRadius);
    yield return null;
  }
  
  [UnityTest]
  public IEnumerator LowRange()
  {
    var initialRadius = _bombController.explosionRadius;

    _bombController.LowRange();
    Assert.AreEqual(1, _bombController.explosionRadius);

    yield return new WaitForSeconds(11);
    Assert.AreEqual(initialRadius, _bombController.explosionRadius);
    yield return null;
  }
  [UnityTest]
  public IEnumerator NoBomb()
  {
    _bombController.NoBomb();
    Assert.IsTrue(_bombController.dontplacebomb);

    yield return new WaitForSeconds(6);
    Assert.IsFalse(_bombController.dontplacebomb);
    yield return null;
  }



}