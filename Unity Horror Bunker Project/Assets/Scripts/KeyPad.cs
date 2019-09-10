using UnityEngine;
using System.Collections;

public class KeyPad : MonoBehaviour
{

    public string curPassword = "12345";
    public string input;
    public bool onTrigger; //checks if the player collides with the collider
    public bool doorOpen; //checks if the door is opened
    public bool keypadScreen;
    public Transform doorHinge;



    void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        onTrigger = false;
        keypadScreen = false;
        input = "";
    }

    void Update()
    {

        UpdateGui();


        if (input == curPassword)
        {
            doorOpen = true;
        }

        if (doorOpen)
        {
            var newRot = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(0.0f, 90.0f, 0.0f), Time.deltaTime * 250);
            doorHinge.rotation = newRot;
        }
    }

    void OnGUI()
    {
        if (!doorOpen)
        {
            if (onTrigger)
            {
                GUI.Box(new Rect(0, 0, 400, 50), "TIP: Press 'E' to open keypad"); //displays the "Tip" GUI to let the player know what to do

                if (Input.GetKeyDown(KeyCode.E))
                {
                    keypadScreen = true; 
                    onTrigger = false;
                }
            }

            if (keypadScreen) //displays the rectangle responsible for the keypad
            {
                GUI.Box(new Rect(0, 0, 640, 70), "");
                GUI.Box(new Rect(10, 10, 620, 50), input);
                GUI.Label(new Rect(10, 60, 200, 50), "'Esc' to exit");

                if (Input.GetKeyDown(KeyCode.Escape)) //exits the rectangle
                {
                    keypadScreen = false;
                }

            }
        }
    }

    void UpdateGui() //This function allows the player to enter a keycode.
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            input = input + "0";
        }


        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            input = input + "1";
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            input = input + "2";
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            input = input + "3";
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            input = input + "4";
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            input = input + "5";
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            input = input + "6";
        }

        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            input = input + "7";
        }

        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            input = input + "8";
        }

        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            input = input + "9";
        }

    }
}