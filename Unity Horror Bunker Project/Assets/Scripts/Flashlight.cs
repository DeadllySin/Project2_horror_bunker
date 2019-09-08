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
    public GameObject LightObj;
    private float maxBattery;
    public int packBattery;
    private int maxPackBattery;
    public float currentBattery;
    public float addBattery;
    private float decreaseBattery;
    public bool flashlightEnabled;
   

    public void Start()
    {
        Instance = this;
        addBattery = 100;
        currentBattery = 0;
        packBattery = 0;
        maxPackBattery = 5;
        maxBattery = 100;
        decreaseBattery = 1f;
        BatterySlider.maxValue = maxBattery;
        currentBattery = Mathf.Clamp(currentBattery, 0, 100);

        UpdateUI();
    }

    public void Update()
    {
        UpdateUI();

        //Equip
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlightEnabled =! flashlightEnabled;
        }

        //Reload battery
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (packBattery > 0)
            {
                packBattery -= 1;
                currentBattery += addBattery;
            }
            if (packBattery <= 0)
            {
                packBattery = 0;
                Debug.Log("You have no more batteries!");
            }
        }

        if (currentBattery > maxBattery)
        {
            currentBattery = maxBattery;
        }

        if (flashlightEnabled)
        {
            flashlight.SetActive(true);

            if (currentBattery <= 0)
            {
                LightObj.SetActive(false);
                currentBattery = 0;
            }
            if (currentBattery > 0)
            {
                LightObj.SetActive(true);
                currentBattery -= decreaseBattery * Time.deltaTime;
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
