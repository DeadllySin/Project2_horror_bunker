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
    public float maxLight, lowLight, currentLight;
    public float lightSpeed;
    private float maxBattery;
    public int packBattery;
    private int maxPackBattery;
    public float currentBattery;
    public float addBattery;
    private float decreaseBattery;
    public bool flashlightEnabled;

     void Awake()
    {
        _light = GetComponentInChildren<Light>();
    }

    public void Start()
    {
        maxLight = 3.5f;
        lowLight = 0.2f;
        lightSpeed = 1f;
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
                _light.intensity = lowLight;
                currentBattery = 0;
            }
            if (currentBattery > 0 && Input.GetMouseButton(1))
            {
                currentBattery -= decreaseBattery * Time.deltaTime;
                _light.intensity = Mathf.Lerp(maxLight, lowLight, Mathf.PingPong(Time.time, lightSpeed));
            }
            else
            {
                _light.intensity = lowLight;
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
