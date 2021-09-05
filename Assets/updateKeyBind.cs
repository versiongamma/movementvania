using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

public class updateKeyBind : MonoBehaviour
{

    public int selector;

    public void setKeyBind(int sel)
    {
        selector = sel;
        var inputField = GameObject.Find("InputTextField").GetComponent<InputField>();
        inputField.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        var se = new InputField.OnChangeEvent();
        se.AddListener(finalSetKeyBind);
        inputField.onValueChanged = se;
        EventSystem es = EventSystem.current;
        es.SetSelectedGameObject(GameObject.Find("InputTextField"));
    }

    private void finalSetKeyBind(string arg0)
    {
        int key = (int)(char)arg0[0];
        string keyString;
        switch (selector) {
            case 0:
                keyString = "leftKey";
                break;
            case 1:
                keyString = "rightKey";
                break;
            case 2:
                keyString = "jumpKey";
                break;
            case 3:
                keyString = "dashKey";
                break;
            case 4:
                keyString = "mapKey";
                break;
            case 6:
                keyString = "inventoryKey";
                break;
            default:
                // Error here!
                return;
        }
        Debug.Log(keyString);
        InputController inputController = GetComponent<InputController>();
        if (inputController != null)
        {
            PlayerPrefs.SetInt(keyString, key);
            inputController.setShouldUpdateKeyBindings(true);
            inputController = null;
        }
        else
            Debug.Log("Got NULL pointer????\nShould probably notify user that we failed to save their key!");
        GameObject.Find("InputTextField").GetComponent<InputField>().transform.position = new Vector3(-1500f, Screen.height * 0.5f, 0);
        GameObject.Find("InputTextField").GetComponent<InputField>().text = "Press a key/button";
    }
}
