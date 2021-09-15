using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // controllerActive : Boolean for whether a controller is being used or not
    public bool controllerActive = false;
    // shouldUpdateKeyBindings : Boolean for whether or not the keybindings have been updated in the settings menu
    public static bool shouldUpdateKeyBindings = false;
    public static float smoothingValue = 0;
    public static int smoothingValueMax = 1;
    public static int smoothingValueMin = -1;

    // Key bindings
    // ADD IN CONTROLLER PRESETS AS WELL
    public static KeyCode jump;
    public static KeyCode dash;
    public static KeyCode swing;
    public static KeyCode left;
    public static KeyCode right;
    public static KeyCode up;
    public static KeyCode down;
    public static KeyCode map;
    public static KeyCode inventory;

    public void updateKeyBindingsFromFile()
    {
        if (isControllerActive())
        {
            jump = (KeyCode) PlayerPrefs.GetInt("jumpKeyController", (int)KeyCode.JoystickButton0);
            dash = (KeyCode) PlayerPrefs.GetInt("dashKeyController", (int)KeyCode.JoystickButton5);
            swing = (KeyCode) PlayerPrefs.GetInt("swingKeyController", (int)KeyCode.JoystickButton4);
            /*
            left = ;
            right = ;
            */
            map = (KeyCode) PlayerPrefs.GetInt("mapKeyController", (int)KeyCode.JoystickButton3);
            inventory = (KeyCode) PlayerPrefs.GetInt("mapKeyController", (int)KeyCode.JoystickButton2);
             
        }
        else
        {
            jump = (KeyCode) PlayerPrefs.GetInt("jumpKey", (int)KeyCode.Space);
            dash = (KeyCode) PlayerPrefs.GetInt("dashKey", (int)KeyCode.Z);
            swing = (KeyCode) PlayerPrefs.GetInt("swingKey", (int)KeyCode.LeftShift);
            left = (KeyCode) PlayerPrefs.GetInt("leftKey", (int)KeyCode.A);
            right = (KeyCode) PlayerPrefs.GetInt("rightKey", (int)KeyCode.D);
            up = (KeyCode) PlayerPrefs.GetInt("upKey", (int)KeyCode.W);
            down = (KeyCode) PlayerPrefs.GetInt("downKey", (int)KeyCode.S);
            map = (KeyCode) PlayerPrefs.GetInt("mapKey", (int)KeyCode.M);
            inventory = (KeyCode) PlayerPrefs.GetInt("inventory", (int)KeyCode.I);
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
    public bool isSwingActive()
    {
        return Input.GetKey(swing);
    }
    public bool isSwingDown()
    {
        return Input.GetKeyDown(swing);
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
        if (isControllerActive())
            return Input.GetAxis("Horizontal");
        float ret = 0;
        if (Input.GetKey(left))
        {
            ret += -1f;
        }
        if (Input.GetKey(right))
        {
            ret += 1f;
        };
        return ret;
    }

    public float getVerticalAxis()
    {
        if (isControllerActive())
            return Input.GetAxis("Vertical");
        float ret = 0;
        if (Input.GetKey(down))
        {
            ret += -1f;
        }
        if (Input.GetKey(up))
        {
            ret += 1f;
        };
        return ret;
    }

    public bool controllerButtonPressed()
    {
        for (int i = 330; i < 350; i++)
        {
            if (Input.GetKey((KeyCode)i))
                return true;
        }
        if (Input.GetAxis("Horizontal") > 0.6f || Input.GetAxis("Horizontal") < -0.6f)
        {
            return true;
        }
        else 
        {
            if (isControllerActive())
            {
                updateControllerActive(false);
                return true;
            }
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
