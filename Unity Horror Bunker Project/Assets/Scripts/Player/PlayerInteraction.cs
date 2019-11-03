using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask = 1;

    [SerializeField]
    private float interactionDistance = 2.5f;

    private RaycastHit hit;
    private bool hasHit;

    private void Update()
    {
        if (Time.frameCount % 5 == 0)
        {
            hasHit = DoRayCast();
        }

        if (Input.GetKeyDown(KeyCode.E) && hasHit)
        {
            hit.collider.gameObject.GetComponent<Interactable>().Interact();
        }
    }

    private bool DoRayCast()
    {
        return Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance, layerMask);
    }
}