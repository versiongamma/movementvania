using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayKeyCode : MonoBehaviour {
    public static void Display(Text text, Image img, Key key) {
        if (InputController.controllerActive) {
            img.gameObject.SetActive(true);

            int code = 0;
            char[] keyString;
            switch (key) {
                case Key.Jump:
                    keyString = InputController.jump.ToString().ToCharArray();
                    code = int.Parse(keyString[keyString.Length - 1].ToString());
                    break;
                case Key.Dash:
                    keyString = InputController.dash.ToString().ToCharArray();
                    code = int.Parse(keyString[keyString.Length - 1].ToString());
                    break;
                case Key.Swing:
                    keyString = InputController.swing.ToString().ToCharArray();
                    code = int.Parse(keyString[keyString.Length - 1].ToString());
                    break;
            }
            
            img.sprite = Resources.Load<Sprite>("Icons/" + code.ToString());
        } else {
            text.gameObject.SetActive(true);
            text.text = InputController.jump.ToString().ToUpper();
        }
    }
}
