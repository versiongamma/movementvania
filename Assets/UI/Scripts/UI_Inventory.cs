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

    private void Awake()
    {
        PowerupSlotContainer = transform.Find("PowerupSlotContainer");
        PowerupSlotTemplate = PowerupSlotContainer.Find("PowerupSlotTemplate");
        PowerupSlotTemplateText = PowerupSlotTemplate.Find("Text");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        int x = 0;
        int y = 0;
        float powerupSlotCellSize = 5f;

        foreach (string powerupName in inventory.GetPowerupList())
        {
            PowerupSlotTemplateText.GetComponent<Text>().text = powerupName;

            RectTransform powerupSlotRectTransform = Instantiate(PowerupSlotTemplate, PowerupSlotContainer).GetComponent<RectTransform>();
            powerupSlotRectTransform.gameObject.SetActive(true);
            powerupSlotRectTransform.anchoredPosition = new Vector2(x * powerupSlotCellSize, y);

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
