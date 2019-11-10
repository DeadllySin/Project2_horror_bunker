using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadInteractionTarget : InteractionTarget
{
    [SerializeField]
    private InteractionTarget target = null;

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

    public string userInput;

    
    public override void Interact(Interaction.InteractionType interactionType, string value = null)
    {
        if (locked && string.IsNullOrEmpty(value))
        {
            return;
        }

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
        if (!locked)
        {
            Open();
            return;
        }
        
        StopCoroutine("ResetInput");

        userInput = userInput + value;
        keypadText.text = userInput;

        if (userInput == accessCode)
        {
            locked = false;
        }



        StartCoroutine("ResetInput", inputResetTime);
    }

    private IEnumerator ResetInput(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (locked)
        {
            keypadText.text = "LOCKED";
        }
        else
        {
            keypadText.text = "UNLOCKED";
        }
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
            target.Interact(Interaction.InteractionType.Open);
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