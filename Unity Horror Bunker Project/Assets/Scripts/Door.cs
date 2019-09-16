using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform player;
    public Transform door;
    public bool doorOpen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpen)
        {
            var newRot = Quaternion.RotateTowards(door.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 60);
            door.rotation = newRot;
        }
        else if (!doorOpen)
        {
            var newRot = Quaternion.RotateTowards(door.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 60);
            door.rotation = newRot;
        }
    }

     void OnMouseDown()
    {
        if ((transform.position - player.position).magnitude < 2)
        {
            Debug.Log("you clicked door");
            doorOpen = !doorOpen;
        }
    }
}
