using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryUI inventoryUI;

    public bool AddItem(string itemName, Sprite itemIcon)
    {
        // Delegate item addition to Inventory UI
        return inventoryUI.AddItemToInventory(itemName, itemIcon);
    }
}