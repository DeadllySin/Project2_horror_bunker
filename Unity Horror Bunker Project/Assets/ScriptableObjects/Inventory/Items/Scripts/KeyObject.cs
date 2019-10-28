using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key Object", menuName = "Inventory System/Items/Keys")]
public class KeyObject : ItemObject
{
    public GameObject doorID;

    public void Awake()
    {
        type = ItemType.Key;
        doorID.GetComponent<Door>().doorLocked = false; //To be fixed !
    }

    
}
