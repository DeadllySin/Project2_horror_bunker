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
    [SerializeField] private GameObject[] buttons; 


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
                GUI.Box(new Rect(0, 0, 400, 50), "TIP: Press 'E' to push a button"); //displays the "Tip" GUI to let the player know what to do

            }

        }
    }

    

    void onClickedButton()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            if (hit.transform != null) 
            {
                for (int i = 0; i < 9; i++)
                {
                    if (hit.transform.gameObject == buttons[i])
                    {
                        input += i.ToString();
                    }
                }
            }
        }
    }
}