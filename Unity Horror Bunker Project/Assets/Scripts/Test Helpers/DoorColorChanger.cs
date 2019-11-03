using UnityEngine;

public class DoorColorChanger : MonoBehaviour
{
    public GameObject door;
    private DoorInteractionTarget doorInteractionTarget;
    private Renderer myRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        doorInteractionTarget = GetComponent<DoorInteractionTarget>();
        myRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        switch (doorInteractionTarget.GetState())
        {
            case "locked":
                myRenderer.material.SetColor("_Color", Color.red);
                break;

            case "open":
                myRenderer.material.SetColor("_Color", Color.white);
                break;

            case "closed":
                myRenderer.material.SetColor("_Color", Color.green);
                break;
        }
    }
}