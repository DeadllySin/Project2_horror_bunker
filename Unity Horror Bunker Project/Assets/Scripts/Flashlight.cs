using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

    public Slider BatterySlider;
    public Text BatteryPack;
    public static Flashlight Instance;
    public GameObject flashlight;
    private Light _light;
    public float currentLight;
    public int packBattery;
    private int maxPackBattery;
    public float currentBattery;
    private float increaseBattery, decreaseBattery;
    private bool flashlightEnabled;
    private bool internalLight;
    private bool internalLightActivated;
    private bool externalLight;
    private bool externalLightActivated;

     void Awake()
    {
        _light = GetComponentInChildren<Light>();
    }

    public void Start()
    {
        Instance = this;
        currentBattery = 0f;
        packBattery = 0;
        maxPackBattery = 5;

        // Energy battery regen
        decreaseBattery = 4.5f;
        increaseBattery = 15f;

        // Battery Slide UI 
        BatterySlider.maxValue = 100.0f;
        currentBattery = Mathf.Clamp(currentBattery, 0, 100);

        UpdateUI();
    }

    public void Update()
    {
        UpdateUI();
        currentLight = _light.intensity;

        //Equip
        if (Input.GetKeyDown(KeyCode.E))
        {
            flashlightEnabled =! flashlightEnabled;
        }

        if (flashlightEnabled)
        {
            flashlight.SetActive(true);
        
            //Reload add-on external battery
            if (Input.GetKeyDown(KeyCode.R) && packBattery > 0)
            {
                currentBattery += 100f;
                packBattery -= 1;
               
                // value constraints
                if (packBattery < 0)
                {
                    packBattery = 0;
                }
                if (packBattery > 5)
                {
                    packBattery = 5;
                }
                if (currentBattery > 100)
                {
                    currentBattery = 100f;
                }
              
            }
         

            // Activate Internal battery
            if (Input.GetKeyDown(KeyCode.F))
            {
                internalLight =! internalLight;
            }

            if (internalLight)
            {
                _light.intensity = 3.5f;
                decreaseBattery = 12.5f;
                internalLightActivated = true;
            }
            else
            {
                _light.intensity = 0.5f;
                decreaseBattery = 4.5f;
                internalLightActivated = false;
            }
           

            // Hold to crank flashlight
            if (Input.GetMouseButton(1) && !internalLightActivated)
            {
                currentBattery += increaseBattery * Time.deltaTime;

                if (currentBattery > 100.0f)
                {
                    currentBattery = 100.0f;
                }
            }
            else 
            {
                currentBattery -= decreaseBattery * Time.deltaTime;

                if (currentBattery < 0)
                {
                    currentBattery = 0f;
                    internalLight = false;
                    externalLight = false;
                }
            }

        }
        else
        {
            flashlight.SetActive(false);
        }
    }

    private void UpdateUI()
    {
        BatterySlider.value = currentBattery;

        BatteryPack.text = packBattery + " / 5";
    }
}
