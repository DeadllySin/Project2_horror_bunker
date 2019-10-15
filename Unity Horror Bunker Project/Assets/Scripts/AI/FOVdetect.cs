using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fovdetect : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float maxAngle;
    [SerializeField] private float maxRadius;

    [SerializeField] private float Alert;
     private float AlertOverTimeExposed, AlertOverTimeHidden;

    public bool isExposed;
    private bool isInFov = false;

    private void OnDrawGizmos()
    {
        // To create lines of radius and angle for testing

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        if (!isInFov)
        {
            // if the player is NOT in FOV
            Gizmos.color = Color.red;
        }
        else
        {
            // if the player is in FOV
            Gizmos.color = Color.green;
        }
        
        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);

    }

    public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        Collider[] overLaps = new Collider[30];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overLaps);

        for (int i = 0; i < count; i++)
        {
            if (overLaps[i] != null)
            {

                if (overLaps[i].transform == target)
                {

                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                    if (angle <= maxAngle)
                    {
                        

                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius))
                        {
                            // possible point to place radious sound

                            if (hit.transform == target)
                            {
                                return true;
                            }
                        }

                    }

                }
            }
        }

        return false;
    }

    private void Update()
    {
        isInFov = inFOV(transform, player, maxAngle, maxRadius);

        DetectPlayer();
    }

    void AlertLimiter()
    {
        // limits the value for code optimisation
        if (Alert < 0)
        {
            Alert = 0f;
        }
        else if (Alert > 100f)
        {
            Alert = 100f;
        }
    }

    private void DetectPlayer()
    {
        //// Set the values of detection frequency
        AlertOverTimeExposed = 69.0f;
        AlertOverTimeHidden = 8.0f;

        // Math clamp the values to modify
        Alert = Mathf.Clamp(Alert, 0.0f, 100.0f);

        if (!isInFov)
        {
            // check if target is not in enemy FOV
            Alert -= AlertOverTimeHidden * Time.deltaTime;
        }
        else
        {
            // do something if player is in enemy POV
            Alert += AlertOverTimeExposed * Time.deltaTime;
        }

        // Visual Cue Test
        if (Alert >= 100.0f)
        {
            isExposed = true;
        }
        else if (Alert <= 0.0f)
        {
            isExposed = false;
        }

        AlertLimiter();
    }

}
