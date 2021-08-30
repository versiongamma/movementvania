using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    // controllerActive : Boolean for whether a controller is being used or not
    public bool controllerActive = false;

    // Key bindings
    // EVENTUALLY CHANGE THIS TO READ FROM USER SETTINGS WITH PRESET DEFAULTS
    // ADD IN CONTROLLER PRESETS AS WELL
    KeyCode jump = KeyCode.Space;
    KeyCode dash = KeyCode.Z;

    public void updateJumpBind(KeyCode b) 
    {
        jump = b;
    }
    public void updateDashBind(KeyCode b) 
    {
        dash = b;
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
            if (Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.Z))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    void Start()
    {
        Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {
        if (controllerButtonPressed())
        {
            updateControllerActive(true);
            updateJumpBind(KeyCode.JoystickButton0);
            updateDashBind(KeyCode.JoystickButton1);
        }
        else 
        {
            updateControllerActive(false);
            updateJumpBind(KeyCode.Space);
            updateDashBind(KeyCode.Z);
        }
    }
}
