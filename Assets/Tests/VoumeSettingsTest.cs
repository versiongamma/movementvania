using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEditor;

public class VoumeSettingsTest : MonoBehaviour
{
    GameObject sfxSliderGO;
    Slider slider;

    [SetUp]
    public void VolumeSettingsTestSetup() {
        sfxSliderGO = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/UI/Prefabs/SfxSlider.prefab");
        sfxSliderGO = GameObject.Instantiate(sfxSliderGO);
        slider = sfxSliderGO.GetComponent<Slider>();
    }

    [UnityTest]
    public IEnumerator VolumeSettingsTestWithEnumeratorPasses()
    {
        yield return new WaitForFixedUpdate();
        Assert.AreEqual(slider.value, PlayerPrefs.GetFloat("SfxVolume"));
    }

}
