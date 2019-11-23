using System.Collections.Generic;
using UnityEngine;

public static class InventoryManager
{
    public static List<Item> Inventory;

    public static void Initialize()
    {
        Inventory = new List<Item>();
    }

    public static void AddItem(Item item)
    {
        Inventory.Add(item);
    }

    public static void RemoveItem(Item item)
    {
        Inventory.Remove(item);
    }

    public static bool HasItem(Item item)
    {
        // TODO: also check if the item is currently selected
        
        return Inventory.Contains(item);
    }
}