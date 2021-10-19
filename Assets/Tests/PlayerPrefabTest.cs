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
        var playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Player/Player.prefab");
        bool playerPrefabFound = false;

        if (playerPrefab != null) {
            playerPrefabFound = true;
            Debug.Log("Player Prefab Found");
        }
        else if (playerPrefab == null) {
            playerPrefabFound = false;
            Debug.Log("Player Prefab Not Found");
        }

        Assert.AreNotEqual(playerPrefabFound, false);
        yield return new WaitForSeconds(3f);
    }
}