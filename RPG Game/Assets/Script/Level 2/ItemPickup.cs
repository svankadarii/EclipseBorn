using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName = "Item Name";
    public Sprite itemIcon;

    private void OnTriggerStay(Collider other)
    {
        // Check if player is near and presses E
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            // Try to add item to inventory
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                // Attempt to add item
                if (inventory.AddItem(itemName, itemIcon))
                {
                    // Item successfully added, destroy pickup
                    Destroy(gameObject);
                }
            }
        }
    }
}