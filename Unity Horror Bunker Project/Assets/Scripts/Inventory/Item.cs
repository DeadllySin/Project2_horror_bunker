using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Collectibles/Item")]
public class Item : ScriptableObject
{    public enum ItemType
    {
        Key,
        Consumable,
        Puzzle
    }

    [SerializeField]
    private Sprite inventoryImage;
    
    [SerializeField]
    private ItemType itemtype;
    
    [SerializeField]
    private string itemName;

    [SerializeField]
    [TextArea(5, 15)]
    private string Description;

    public Sprite InventoryImage1 { get => inventoryImage; set => inventoryImage = value; }
    public ItemType Itemtype1 { get => itemtype; set => itemtype = value; }
    public string ItemName1 { get => itemName; set => itemName = value; }
}