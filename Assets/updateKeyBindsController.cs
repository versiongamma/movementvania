using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class updateKeyBindsController : MonoBehaviour
{

    public int selector;
    public int pressedKey = 0;

    public void setKeyBindController(int sel)
    {
        selector = sel;
        var inputField = GameObject.Find("InputTextField").GetComponent<InputField>();
        inputField.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
        EventSystem es = EventSystem.current;
        es.SetSelectedGameObject(GameObject.Find("InputTextField"));
        inputField.text = "~Press a key/button";
        int pressedKey = 0;
        StartCoroutine("getControllerKeyPress");
    }

    IEnumerator getControllerKeyPress()
    {
        while (pressedKey < 330 || pressedKey > 350)
        {
            for (int i = 330; i < 350; i++)
            {
                if (Input.GetKey((KeyCode)i))
                {
                    pressedKey = i;
                    break;
                }
            }
            yield return null; // Sleep for a frame before checking keys again, ensures the game doesn't lock up eternally
        }
        if (pressedKey != 0)
            finalSetKeyBind(pressedKey);
    }

    private void finalSetKeyBind(int key)
    {
        Debug.Log((KeyCode)key); 
        string keyString;
        switch (selector)
        {
            case 2:
                keyString = "jumpKeyController";
                break;
            case 3:
                keyString = "dashKeyController";
                break;
            case 4:
                keyString = "mapKeyController";
                break;
            case 5:
                keyString = "inventoryKeyController";
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
            inputController.updateControllerActive(true);
            switch (selector)
            {
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
