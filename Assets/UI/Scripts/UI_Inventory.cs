using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform PowerupSlotContainer;
    private Transform PowerupSlotTemplate;
    private Transform PowerupSlotTemplateText;

    // Find the UI components on awake
    private void Awake()
    {
        PowerupSlotContainer = transform.Find("PowerupSlotContainer");
        PowerupSlotTemplate = PowerupSlotContainer.Find("PowerupSlotTemplate");
        PowerupSlotTemplateText = PowerupSlotTemplate.Find("Text");
    }

    // Set inventory constructor
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }
    
    // Function called when inventory is updated from equipment function 
    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        float powerupSlotCellSize = 5f;

        // Creates an instance of the template inventory component for each 
        foreach (string powerupName in inventory.GetPowerupList())
        {
            PowerupSlotTemplateText.GetComponent<Text>().text = powerupName;

            RectTransform powerupSlotRectTransform = Instantiate(PowerupSlotTemplate, PowerupSlotContainer).GetComponent<RectTransform>();
            powerupSlotRectTransform.gameObject.SetActive(true);
            powerupSlotRectTransform.anchoredPosition = new Vector2(x * powerupSlotCellSize, y);

            // Allow one per line (since we display the powerups in a vertical menu
            x++;
            if (x > 0)
            {
                x = 0;
                y--;
            }
            Debug.Log("Refreshing inventory item " + powerupName);
        }
    }
}
