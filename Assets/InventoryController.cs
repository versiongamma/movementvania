using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] public GameObject inventoryUI;
    [SerializeField] public bool viewInventory;

    // Checks if the user has pressed the key for inventory
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            viewInventory = !viewInventory;

            if (viewInventory)
            {
                InventoryView();
            }
            else
            {
                InventoryHide();
            }
        }
    }

    // Shows inventory
    public void InventoryView()
    {
        inventoryUI.SetActive(true);
    }

    // Hides inventory
    public void InventoryHide()
    {
        inventoryUI.SetActive(false);
    }
}
