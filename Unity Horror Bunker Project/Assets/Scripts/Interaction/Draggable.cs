using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField]
    private float DragInteractionDistance = 3.0f;
    
    private Vector3 mOffSet;
    private float mZCoord;

    private void OnMouseDown()
    {
        if ((transform.position - Camera.main.transform.position).magnitude < DragInteractionDistance)
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffSet = gameObject.transform.position - GetMouseWorldPos();
        }
    }

    private void OnMouseDrag()
    {
        // TODO: Needs collision checking while dragging, maybe inertia when released?
        
        if ((transform.position - Camera.main.transform.position).magnitude < DragInteractionDistance)
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