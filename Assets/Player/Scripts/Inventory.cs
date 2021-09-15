using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<string> powerupList;

    public Inventory()
    {
        powerupList = new List<string>();
    }

    public void AddPowerup(string powerupName)
    {
        powerupList.Add(powerupName);
        Debug.Log(powerupName);
    }

    public List<string> GetPowerupList()
    {
        return powerupList;
    }
}
