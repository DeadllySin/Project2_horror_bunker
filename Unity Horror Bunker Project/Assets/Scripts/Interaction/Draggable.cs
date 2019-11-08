using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField]
    private float DragInteractionDistance = 3.0f;

    [SerializeField]
    private float dragForce = 10.0f;

    [SerializeField]
    private float throwForce = 10.0f;
    
    private Vector3 mOffSet;
    private float mZCoord;
    private Rigidbody myRigidBody;
    private bool dragging = false;


    private void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if ((transform.position - Camera.main.transform.position).magnitude < DragInteractionDistance)
        {
            if (Input.GetMouseButtonDown(1))
            {
                myRigidBody.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
                dragging = false;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
                mOffSet = gameObject.transform.position - GetMouseWorldPos();
                dragging = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
            }

            if (dragging)
            {
                myRigidBody.velocity = (GetMouseWorldPos() + mOffSet - transform.position) * dragForce;
            }
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}