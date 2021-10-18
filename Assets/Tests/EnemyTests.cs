using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class EnemyTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void EnemyTestsSimplePasses()
    {
        
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator EnemyTestsWithEnumeratorPasses()
    {
        //setting the enemy object to a camera
        var enemy = new GameObject();
        enemy.AddComponent(typeof(Camera));
        var camera = enemy.GetComponent<Camera>();
        enemy = GameObject.Instantiate(enemy);

        //testing the basic enemy prefab
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("");
        prefab = GameObject.Instantiate(prefab, new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));

        //testing the following enemy prefab
        var prefab2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Enemy/FollowingEnemy.prefab");
        prefab2 = GameObject.Instantiate(prefab, new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));

        //testing the jumper enemy prefab
        var prefab3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Enemy/Jumper.prefab");
        prefab3 = GameObject.Instantiate(prefab, new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));

        //testing the patroller enemy prefab
        var prefab4 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Enemy/Patroller.prefab");
        prefab4 = GameObject.Instantiate(prefab, new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));

        //testing the shooter enemy prefab
        var prefab5 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Enemy/ShooterEnemy.prefab");
        prefab5 = GameObject.Instantiate(prefab, new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));

        //testing the wall crawler enemy prefab
        var prefab6 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Enemy/WallCrawler.prefab");
        prefab6 = GameObject.Instantiate(prefab, new Vector3(0, 0, 10), new Quaternion(0, 180, 0, 0));

        yield return new WaitForSeconds(3f);
    }
}
