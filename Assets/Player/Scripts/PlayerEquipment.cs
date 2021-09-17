using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Power up names, with values that correspond to the index in the PlayerEquipment's array [powerups] that each powerup corresonds to
public enum PowerUps {
        DoubleJump = 0,
        Dash = 1,
        WallJump = 2,
        Swing = 3
    }

// Holds the logic for not letting the player perfom specific actions before they've collected a powerup, 
// and the functions that give the player those powerups
public class PlayerEquipment : MonoBehaviour {

    [Header("Starting PowerUps")]
    [SerializeField] private bool doubleJump;
    [SerializeField] private bool dash;
    [SerializeField] private bool wallJump;
    [SerializeField] private bool swing;
    private bool[] powerups;
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    // On awake initialises inventory and updates UI inventory
    void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    // Initialises the current powerup set to the serialized fields. This is mainly for debugging, on 
    // game loads where the player has a set number of powerups, they will be inited from setPowerUps
    // In production, the serialized fields will all be negative
    void Start() {
        powerups = new bool[]{doubleJump, dash, wallJump, swing};
    }

    public bool GetPowerupState(PowerUps index) { return powerups[(int)index]; }

    // Gives the player the powerup specified 
    public void GivePowerup(PowerUps index) { 
        // Adds powerup as a string to the inventory array
        inventory.AddPowerup("" + index);
        // Updates UI inventory
        uiInventory.SetInventory(inventory);

        powerups[(int)index] = true; 
        Debug.Log(powerups[((int)PowerUps.DoubleJump)]);
    }

    public bool[] getPowerUps() 
    {
        return this.powerups;
    }

    public void setPowerUps(bool[] pu) 
    {
        this.powerups = pu;
    }

}
