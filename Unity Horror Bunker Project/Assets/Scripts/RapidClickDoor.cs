using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidClickDoor : MonoBehaviour
{
    public float rotationMultiplier;
    public float rotationSpeed;
    public float interactRange;
    public bool openingForward;
    private bool isInteractable;
    public GameObject canInteractText; // Maybe panel, that's a placeholder
    private Transform player;
    private float clicksNum;
    private Vector3 orgRot;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        orgRot = transform.localEulerAngles;
        StartCoroutine("Rotate");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= interactRange && ((transform.localEulerAngles.y > 270 && transform.localEulerAngles.y < 360) || transform.localEulerAngles.y == 0 || (transform.localEulerAngles.y < 90 && transform.localEulerAngles.y != 0)))
    {
            canInteractText.SetActive(true);
            isInteractable = true;
        }
        else
        {
            canInteractText.SetActive(false);
            isInteractable = false;
        }

        if (isInteractable)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if(openingForward)
                {
                    if (transform.localEulerAngles.y < 90)
                    {
                        clicksNum++;
                    }
                    else
                    {
                        StopCoroutine("Rotate");
                    }
                }
                else
                {
                    if ((transform.localEulerAngles.y > 270 && transform.localEulerAngles.y < 360) || transform.localEulerAngles.y == 0)
                    {
                        clicksNum++;
                    }
                    else
                    {
                        StopCoroutine("Rotate");
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }

    IEnumerator Rotate()
    {
        if (openingForward)
        {
            while (true)
            {
                Vector3 rot = new Vector3(transform.localEulerAngles.x, orgRot.y + rotationMultiplier * clicksNum, transform.localEulerAngles.z);
                if (transform.localEulerAngles.y < rot.y + 1 && transform.localEulerAngles.y > rot.y -1)
                {
                    
                }
                else
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + rotationSpeed * Time.deltaTime, transform.localEulerAngles.z);
                }
                yield return null;
            }
        }
        else
        {
            while (true)
            {
                Vector3 rot = new Vector3(transform.localEulerAngles.x, 360 - rotationMultiplier * clicksNum, transform.localEulerAngles.z);
                if (transform.localEulerAngles.y < rot.y + 1 && transform.localEulerAngles.y > rot.y - 1)
                {
                    
                }
                else
                {
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - rotationSpeed * Time.deltaTime, transform.localEulerAngles.z);
                }
                yield return null;
            }
        }
    }
}
