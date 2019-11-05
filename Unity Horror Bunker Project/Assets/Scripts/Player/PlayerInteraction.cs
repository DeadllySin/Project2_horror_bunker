using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask = 1;

    [SerializeField]
    private float interactionDistance = 2.5f;

    [SerializeField]
    private float forceAddPerInteraction = 0.2f;

    [SerializeField]
    private float forceReleasePerSecond = 2.5f;

    private UIManager myUImanager;

    private RaycastHit hit;
    private Interactable interactable;
    private bool hasHit;
    private bool forceBuildUp = false;
    public float currentForce = 0;

    void Start()
    {
        myUImanager = GameObject.FindObjectOfType<UIManager>();
    }

    void Update()
    {
        if (Time.frameCount % 5 == 0)
        {
            FindInteractables();
        }

        if (Input.GetKeyDown(KeyCode.E) && hasHit && interactable != null)
        {
            if (interactable.NeedsForce == false)
            {
                interactable.Interact();
            }
            else
            {
                BuildUpForce();
            }
        }

        if (forceBuildUp)
        {
            ReleaseForce();
        }
    }

    private void BuildUpForce()
    {
        forceBuildUp = true;
        currentForce += forceAddPerInteraction;
        if (currentForce >= interactable.NeedsForceAmount)
        {
            interactable.Interact();
            currentForce = 0f;
            forceBuildUp = false;
        }
    }

    private void FindInteractables()
    {
        if (hasHit = DoRayCast())
        {
            interactable = hit.collider.gameObject.GetComponent<Interactable>();
            myUImanager.CrosshairInteract(true);
        }
        else
        {
            interactable = null;
            myUImanager.CrosshairInteract(false);
        }
    }

    private bool DoRayCast()
    {
        return Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance, layerMask);
    }

    private void ReleaseForce()
    {
        currentForce -= forceReleasePerSecond * Time.deltaTime;
        if (currentForce <= 0)
        {
            currentForce = 0f;
            forceBuildUp = false;
        }
    }
}