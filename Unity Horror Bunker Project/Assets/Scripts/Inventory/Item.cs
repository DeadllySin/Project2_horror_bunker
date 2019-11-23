using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Collectibles/Item")]
public class Item : ScriptableObject
{    public enum ItemType
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

    [TextArea(5, 15)]
    public string Description;
}