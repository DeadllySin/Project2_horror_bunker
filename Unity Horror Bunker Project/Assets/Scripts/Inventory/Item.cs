using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
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

    [TextArea(5, 15)]
    public string Description;
}