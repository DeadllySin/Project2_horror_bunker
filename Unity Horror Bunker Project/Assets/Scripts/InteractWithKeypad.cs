using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithKeypad : MonoBehaviour
{
    public string curPassword = "54321";
    public string input;
    public bool onTrigger; //checks if the player collides with the collider
    public bool doorOpen; //checks if the door is opened
    public bool keypadScreen;
    public Transform doorHinge;
    [SerializeField] private GameObject button1, button2, button3, button4, button5, button6, button7, button8, button9, button0; // to be refactored with a loop


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
        if (Input.GetKeyDown(KeyCode.E))
        {
            onClickedButton();

        }

        if (input == curPassword)
        {
            doorOpen = true;
        }

        if (doorOpen)
        {
            var newRot = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 25);
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

            }

        }
    }

    

    void onClickedButton()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            if (hit.transform != null) // to be refactored with switch statements and loops
            { 
                if (hit.transform.gameObject == button1)
                    input += "1";
                else if (hit.transform.gameObject == button2)
                    input += "2";
                else if (hit.transform.gameObject == button3)
                    input += "3";
                else if (hit.transform.gameObject == button4)
                    input += "4";
                else if (hit.transform.gameObject == button5)
                    input += "5";
                else if (hit.transform.gameObject == button6)
                    input += "6";
                else if (hit.transform.gameObject == button7)
                    input += "7";
                else if (hit.transform.gameObject == button8)
                    input += "8";
                else if (hit.transform.gameObject == button9)
                    input += "9";
            }
        }
    }
}