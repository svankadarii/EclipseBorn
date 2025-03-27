using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image[] itemImages;
    public Text[] itemNameTexts;

    public bool AddItemToInventory(string itemName, Sprite itemIcon)
    {
        // Find first empty inventory slot
        for (int i = 0; i < itemImages.Length; i++)
        {
            if (itemImages[i].sprite == null)
            {
                // Fill the slot
                itemImages[i].sprite = itemIcon;
                itemImages[i].color = Color.white;
                itemNameTexts[i].text = itemName;
                return true;
            }
        }
        
        Debug.Log("Inventory is full!");
        return false;
    }
}
