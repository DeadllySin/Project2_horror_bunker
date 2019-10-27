using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{
    public InventoryObject inventory;

    private void OnTriggerEnter(Collider other) //collects the item
    {
        var item = other.GetComponent<InventoryItem>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit() //clears the inventory for testing purposes / TO DO: save/load function
    {
        inventory.Container.Clear();
    }
}
