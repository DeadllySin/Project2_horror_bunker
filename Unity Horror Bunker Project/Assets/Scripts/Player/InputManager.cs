using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool playerCanMove;

    private void Start()
    {
        StartPlayerMovement();
    }

    public bool PlayerCanMove()
    {
        return playerCanMove;
    }

    public void StopPlayerMovement()
    {
        Cursor.lockState = CursorLockMode.None;
        playerCanMove = false;
    }

    public void StartPlayerMovement()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCanMove = true;
    }
}
