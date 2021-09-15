using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] public GameObject inventoryUI;
    [SerializeField] public bool viewInventory;

    // Update is called once per frame
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

    public void InventoryView()
    {
        inventoryUI.SetActive(true);
    }

    public void InventoryHide()
    {
        inventoryUI.SetActive(false);
    }
}
