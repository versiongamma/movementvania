using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PowerUps {
        DoubleJump = 0,
        Dash = 1
    }
public class PlayerEquipment : MonoBehaviour {
    // Start is called before the first frame update

    [Header("Starting PowerUps")]
    [SerializeField] private bool doubleJump;
    [SerializeField] private bool dash;
    private bool[] powerups;

    void Start() {
        powerups = new bool[]{doubleJump, dash};
    }

    public bool GetPowerupState(PowerUps index) { return powerups[(int)index]; }

    public void GivePowerup(PowerUps index) { 
        //Debug.Log(index);
        powerups[(int)index] = true; 
        Debug.Log(powerups[((int)PowerUps.DoubleJump)]);
    }

}
