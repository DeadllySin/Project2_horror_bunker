using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadInteractionTarget : InteractionTarget
{
    [SerializeField]
    private GameObject targetDoor = null;

    [SerializeField]
    private bool locked = true;

    [SerializeField]
    private string accessCode = "1234";

    [SerializeField]
    private float inputResetTime = 5f;

    [SerializeField]
    private bool autoLock = false;

    [SerializeField]
    private float autoLockDelay = 60f;

    [SerializeField]
    private TextMeshPro keypadText = null;

    private string userInput;


    public override void Interact(params object[] values)
    {
        string value = (values[0] as string);
        switch (value)
        {
            case "enter":
                Open();
                break;
            default:
                CheckInput(value);
                break;
        }
    }

    private void CheckInput(string value)
    {
        StopCoroutine("ResetInput");

        userInput = userInput + value;
        keypadText.text = userInput;
        locked = (userInput != accessCode);

        StartCoroutine("ResetInput", inputResetTime);
    }

    private IEnumerator ResetInput(float delay)
    {
        yield return new WaitForSeconds(delay);
        keypadText.text = locked ? "LOCKED" : "UNLOCKED";
        userInput = null;
    }

    private IEnumerator AutoLock(float delay)
    {
        yield return new WaitForSeconds(delay);
        userInput = null;
        locked = true;

        keypadText.text = "LOCKED";
    }

    private void Open()
    {
        if (!locked)
        {
            targetDoor.SendMessage("Open", SendMessageOptions.DontRequireReceiver);
            if (autoLock)
            {
                StartCoroutine("AutoLock", autoLockDelay);
            }
            keypadText.text = "GRANTED";
            StartCoroutine("ResetInput", inputResetTime);
        }
        else
        {
            keypadText.text = "DENIED";
            userInput = null;
        }
    }

    private void UpdateKeypadVisuals()
    {

    }
}