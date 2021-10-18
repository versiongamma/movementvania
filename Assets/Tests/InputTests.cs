using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Collections;
using System.Linq;
using System.IO;
using UnityEditor;

public class InputTests
{
    [Test]
    public void InputTestsSimplePasses()
    {

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator InputTestsWithEnumeratorPasses()
    {
        yield return new WaitForFixedUpdate();

        KeyCode initialKey = KeyCode.W;

        PlayerPrefs.SetInt("upKey", (int)KeyCode.E);

        KeyCode finalKey = (KeyCode)PlayerPrefs.GetInt("upKey", initialKey);

        Assert.AreNotEqual(finalKey, initialKey);
    }
}
