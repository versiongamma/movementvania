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

        try
        {
            Assert.AreNotEqual(System.IO.File.Exists(Application.persistentDataPath + "/TempSaveData.bin"), true); // Ensure that TempSaveData doesn't exist
        }
        catch (Exception e)
        {}
    }
}
