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
        // var player = new GameObject();
        // player.AddComponent(typeof(Camera));
        // var camera = player.GetComponent<Camera>();
        // player = GameObject.Instantiate(player);

        // // Instantiate Player Prefab
        // var playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Player/Player.prefab");
        // playerPrefab = GameObject.Instantiate(playerPrefab, new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));

        // Check that the player exists in the scene
        var playerInScene = GameObject.Find("Player");

        yield return new WaitForSeconds(3f);
    }
}