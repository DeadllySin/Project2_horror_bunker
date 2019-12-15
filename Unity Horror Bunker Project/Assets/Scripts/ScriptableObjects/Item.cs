using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Collectibles/Item")]
public class Item : InteractableItem
{
    public enum ItemType
    {
        Key,
        Consumable,
        Puzzle
    }

    public Sprite InventoryImage;

    public ItemType Itemtype;

    public string ItemName;

    public GameObject WorldPrefab;

    public GameObject PlayerHoldingPrefab;

}