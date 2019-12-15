using System;
using UnityEngine;
using MyBox;

public class Item_Interactable : vp_Interactable
{
    [Space(10)]
    [Header("Custom")]

    [SerializeField]
    private string toolTipText = null;

    [SerializeField]
    private Item giveItem = null;

    public override bool TryInteract(vp_PlayerEventHandler player)
    {
        if (!giveItem)
        {
            return false;
        }

        InventoryManager.AddItem(giveItem);
        Destroy(this.transform.gameObject);

        return true;
    }

}