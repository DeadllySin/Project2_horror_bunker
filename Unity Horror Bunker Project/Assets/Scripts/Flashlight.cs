using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private bool flashlightEnabled;
    [SerializeField]private KeyCode equipFlashlight;

    [SerializeField]private GameObject flashlight;
    [SerializeField]private GameObject lightObject;

    [SerializeField]private float maxEnergy;
    private float currentEnergy;

    private int batteries;
    private GameObject batteryPickedUp;
    private float usedEnergy;

    private void Start()
    {
        currentEnergy = 50 * batteries;
        currentEnergy = maxEnergy;
    }

    private void FixedUpdate()
    {
        maxEnergy = 50 * batteries;
        currentEnergy = maxEnergy;

        if (Input.GetKeyDown(equipFlashlight))
            flashlightEnabled =! flashlightEnabled;

        if (flashlightEnabled)
            {
                flashlight.SetActive(true);

                if (currentEnergy <= 0)
                {
                    lightObject.SetActive(false);
                    batteries = 0;
                }
                if (currentEnergy > 0)
                {
                    lightObject.SetActive(true);
                    currentEnergy -= 0.5f * Time.deltaTime;
                    usedEnergy += 1.0f * Time.deltaTime;
                }

                if (usedEnergy >= 50)
                {
                    batteries -= 1;
                    usedEnergy = 0;
                }
            }
        else
        {
            flashlight.SetActive(false);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Battery")
        {
            batteryPickedUp = other.gameObject;
            batteries += 1;
            Destroy(batteryPickedUp);
        }
    }
}

