using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInput;
    [SerializeField] private string verticalInput;
    [SerializeField] private float movementSpeed;
    private float dashSpeedTimeAtMaxSpeed = 3.0f;
    private float dashSpeedTimeToMaxSpeed = 3.0f;
    private float dashSpeedTimeToDrop = 1.5f;
    private float dashSpeedIncrease = 0.03f;
    private float dashSpeedMax = 4.5f;
    private float dashSpeedDecrease = 0.06f;
    private bool isSpeedGoingUp = false; // don't refactor
    private KeyCode dashKey = KeyCode.LeftShift; // don't refactor

    private CharacterController charController;


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }


    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(dashKey) && isSpeedGoingUp == false)
        {
            StartCoroutine(DashSpeedChange());
        }    

        
        float horizInput = Input.GetAxis(horizontalInput) * movementSpeed;
        float vertInput = Input.GetAxis(verticalInput) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rightMovement);
    }

    private IEnumerator DashSpeedChange()
    {
        // Please speedy fix the bug!
        isSpeedGoingUp = true;
        movementSpeed += dashSpeedIncrease;
        if (movementSpeed > dashSpeedMax)
            movementSpeed = dashSpeedMax;
        yield return new WaitForSeconds(dashSpeedTimeToMaxSpeed);
        movementSpeed = dashSpeedMax;
        yield return new WaitForSeconds(dashSpeedTimeAtMaxSpeed);
        movementSpeed -= dashSpeedDecrease;
        if (movementSpeed < 1.5f)
            movementSpeed = 1.5f;
        yield return new WaitForSeconds(dashSpeedTimeToDrop);
        movementSpeed = 1.5f;
        isSpeedGoingUp = false;
    }
}
