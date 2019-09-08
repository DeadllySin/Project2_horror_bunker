using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Transform player;

    public void Start()
    {
        
    }

    void OnMouseDown()
    {
        if ((transform.position - player.position).magnitude < 2)
        {
            Debug.Log("you pick up a battery!");
            Flashlight.Instance.packBattery += 1;
            Destroy(gameObject);
        }
       
    }
}
