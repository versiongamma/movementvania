using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // controllerActive : Boolean for whether a controller is being used or not
    public bool controllerActive = false;
    // shouldUpdateKeyBindings : Boolean for whether or not the keybindings have been updated in the settings menu
    public static bool shouldUpdateKeyBindings = false;

    // Key bindings
    // ADD IN CONTROLLER PRESETS AS WELL
    public static KeyCode jump;
    public static KeyCode dash;
    public static KeyCode left;
    public static KeyCode right;
    public static KeyCode map;
    public static KeyCode inventory;

    public void updateKeyBindingsFromFile()
    {
        if (isControllerActive())
        {
            jump = (KeyCode) PlayerPrefs.GetInt("jumpKeyController", (int)KeyCode.JoystickButton0);
            dash = (KeyCode) PlayerPrefs.GetInt("dashKeyController", (int)KeyCode.JoystickButton1);
            /*
            left = ;
            right = ;
            map = (KeyCode) PlayerPrefs.GetInt("mapKeyController", (int)KeyCode.JoystickButton3);
            inventory = (KeyCode) PlayerPrefs.GetInt("mapKeyController", (int)KeyCode.JoystickButton2);
             */
        }
        else
        {
            jump = (KeyCode) PlayerPrefs.GetInt("jumpKey", (int)KeyCode.Space);
            dash = (KeyCode) PlayerPrefs.GetInt("dashKey", (int)KeyCode.Z);
            /*
            left = ;
            right = ;
            map = (KeyCode) PlayerPrefs.GetInt("mapKey", (int)KeyCode.M);
            inventory = (KeyCode) PlayerPrefs.GetInt("inventory", (int)KeyCode.I);
             */
        }
    }

    public void updateJumpBind(KeyCode b) 
    {
        jump = b;
    }
    public void updateDashBind(KeyCode b) 
    {
        dash = b;
    }
    public void updateLeftBind(KeyCode b) {
        left = b;
    }
    public void updateRightBind(KeyCode b)
    {
        right = b;
    }
    public void updateMapBind(KeyCode b)
    {
        map = b;
    }
    public void updateInventoryBind(KeyCode b)
    {
        inventory = b;
    }
    public void setShouldUpdateKeyBindings(bool b) 
    {
        shouldUpdateKeyBindings = b;
    }

    public bool isControllerActive() 
    {
        return controllerActive; 
    }

    public void updateControllerActive(bool b) 
    {
        controllerActive = b;
    }

    public bool isJumpActive() 
    {
        return Input.GetKeyDown(jump);
    }

    public bool isDashActive()
    {
        return Input.GetKeyDown(dash);
    }
    public bool isMapActive()
    {
        return Input.GetKeyDown(map);
    }

    public bool isInventoryActive()
    {
        return Input.GetKeyDown(inventory);
    }

    public float getHorizontalAxis() 
    {
        return Input.GetAxis("Horizontal");
    }

    public bool controllerButtonPressed() 
    {
        // This code is extremely messy, will optimize later
        if (Input.GetKeyDown(KeyCode.JoystickButton0) ||
            Input.GetKeyDown(KeyCode.JoystickButton1) ||
            Input.GetKeyDown(KeyCode.JoystickButton2) ||
            Input.GetKeyDown(KeyCode.JoystickButton3) ||
            Input.GetKeyDown(KeyCode.JoystickButton4) ||
            Input.GetKeyDown(KeyCode.JoystickButton5) ||
            Input.GetKeyDown(KeyCode.JoystickButton6) ||
            Input.GetKeyDown(KeyCode.JoystickButton7) ||
            Input.GetKeyDown(KeyCode.JoystickButton8) ||
            Input.GetKeyDown(KeyCode.JoystickButton9) ||
            Input.GetKeyDown(KeyCode.JoystickButton10) ||
            Input.GetKeyDown(KeyCode.JoystickButton11) ||
            Input.GetKeyDown(KeyCode.JoystickButton12) ||
            Input.GetKeyDown(KeyCode.JoystickButton13) ||
            Input.GetKeyDown(KeyCode.JoystickButton14) ||
            Input.GetKeyDown(KeyCode.JoystickButton15) ||
            Input.GetKeyDown(KeyCode.JoystickButton16) ||
            Input.GetKeyDown(KeyCode.JoystickButton17) ||
            Input.GetKeyDown(KeyCode.JoystickButton18) ||
            Input.GetKeyDown(KeyCode.JoystickButton19))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    void Start()
    {
        Debug.Log("Getting user-set keybinds from disk...");
        updateKeyBindingsFromFile();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldUpdateKeyBindings) 
        {
            Debug.Log("Updating keybinds....");
            updateKeyBindingsFromFile();
            setShouldUpdateKeyBindings(false);
        }
        
        if (controllerButtonPressed())
        {
            updateControllerActive(true);
            updateKeyBindingsFromFile();
        }
        else 
        {
            updateControllerActive(false);
            updateKeyBindingsFromFile();
        }
    }
}
