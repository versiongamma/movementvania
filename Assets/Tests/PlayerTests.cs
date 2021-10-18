using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerTestsSimplePasses()
    {
        
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerTestsWithEnumeratorPasses()
    {
        var go = new GameObject();
        go.AddComponent<Rigidbody2D>();
        var originalPosition = go.transform.position.y;

        yield return new WaitForFixedUpdate();
        Assert.AreNotEqual(originalPosition, go.transform.position.y);
    }
}
