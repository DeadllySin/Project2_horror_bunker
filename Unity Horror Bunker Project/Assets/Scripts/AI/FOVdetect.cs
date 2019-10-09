using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVdetect : MonoBehaviour
{
    public Transform player;
    public float maxAngle;
    public float maxRadius;

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
    }
}
