using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class PlayerPrefabTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerPrefabTestWithEnumeratorPasses()
    {
        // Check that the player exists in the scene
        var playerInScene = GameObject.Find("Player");

        yield return new WaitForSeconds(3f);
    }
}