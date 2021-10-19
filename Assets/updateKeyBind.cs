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
        inputField.text = "~Press a key/button";
    }

    private void finalSetKeyBind(string arg0)
    {
        int key = (int)(char)arg0[0];
        if (key == 126)
            return;
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
            case 5:
                keyString = "inventoryKey";
                break;
            case 6:
                keyString = "swingKey";
                break;
            default:
                // Error here!
                return;
        }
        InputController inputController = GetComponent<InputController>();
        if (inputController != null)
        {
            PlayerPrefs.SetInt(keyString, key);
            inputController.setShouldUpdateKeyBindings(true);
            switch (selector)
            {
                case 0:
                    inputController.updateLeftBind((KeyCode)key);
                    break;
                case 1:
                    inputController.updateRightBind((KeyCode)key);
                    break;
                case 2:
                    inputController.updateJumpBind((KeyCode)key);
                    break;
                case 3:
                    inputController.updateDashBind((KeyCode)key);
                    break;
                case 4:
                    inputController.updateMapBind((KeyCode)key);
                    break;
                case 5:
                    inputController.updateInventoryBind((KeyCode)key);
                    break;
                case 6:
                    inputController.updateSwingBind((KeyCode)key);
                    break;
                default:
                    // Error here!
                    return;
            }
            Destroy(inputController);
        }
        else
            Debug.Log("Got NULL pointer????\nShould probably notify user that we failed to save their key!");
        GameObject.Find("InputTextField").GetComponent<InputField>().transform.position = new Vector3(-1500f, Screen.height * 0.5f, 0);
    }
}
