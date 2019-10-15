using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Vector3 mOffSet;
    private float mZCoord;
    public Transform player;

    void OnMouseDown()
    {
        if ((transform.position - player.position).magnitude < 4)
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffSet = gameObject.transform.position - GetMouseWorldPos();
        }
    }

    void OnMouseDrag()
    {
        if ((transform.position - player.position).magnitude < 4)
        {
            transform.position = GetMouseWorldPos() + mOffSet;
        }
        
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
