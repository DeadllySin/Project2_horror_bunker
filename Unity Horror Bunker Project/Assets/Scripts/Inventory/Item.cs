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

    [SerializeField]
    private Sprite InventoryImage;
    
    [SerializeField]
    private ItemType Itemtype;
    
    [SerializeField]
    private string ItemName;

    [SerializeField]
    [TextArea(5, 15)]
    private string Description;

    public Sprite InventoryImage1 { get => InventoryImage; set => InventoryImage = value; }
    public ItemType Itemtype1 { get => Itemtype; set => Itemtype = value; }
    public string ItemName1 { get => ItemName; set => ItemName = value; }
}