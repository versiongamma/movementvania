using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<string> powerupList;

    // Initialises inventory as a list of strings (powerup names)
    public Inventory()
    {
        powerupList = new List<string>();
    }

    // Adds power up to the inventory array
    public void AddPowerup(string powerupName)
    {
        powerupList.Add(powerupName);
        Debug.Log(powerupName);
    }

    // Gets private list
    public List<string> GetPowerupList()
    {
        return powerupList;
    }
}
