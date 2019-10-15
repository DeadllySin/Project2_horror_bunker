using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInput;
    [SerializeField] private string verticalInput;
    [SerializeField] private float movementSpeed;
    private float dashCooldownTime = 5.0f;
    private float dashSpeedTimeAtMaxSpeed = 1.5f;
    private float dashSpeedTimeToMaxSpeed = 3.0f;
    private float dashSpeedTimeToDrop = 1.5f;
    private float dashSpeedMax = 4f;
    private bool isDashOn = false;
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
        if (Input.GetKey(dashKey) && isSpeedGoingUp == false && isDashOn == false)
        {
            StartCoroutine(DashSpeedChange());
        }


        float horizInput = Input.GetAxis(horizontalInput);
        float vertInput = Input.GetAxis(verticalInput);

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed);
    }

    private IEnumerator DashSpeedChange()
    { 
        isSpeedGoingUp = true;
        movementSpeed += 1.5f;
        yield return new WaitForSeconds(1.5f);
        movementSpeed += 1.0f;
        if (movementSpeed > dashSpeedMax)
            movementSpeed = dashSpeedMax;
        yield return new WaitForSeconds(dashSpeedTimeToMaxSpeed);
        movementSpeed = dashSpeedMax;
        yield return new WaitForSeconds(dashSpeedTimeAtMaxSpeed);
        movementSpeed -= 1.5f;
        yield return new WaitForSeconds(0.75f);
        movementSpeed -= 1.5f;
        if (movementSpeed < 1.5f)
            movementSpeed = 1.5f;
        yield return new WaitForSeconds(dashSpeedTimeToDrop);
        movementSpeed = 1.5f;
        
        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldownTime);
        isDashOn = false;
        isSpeedGoingUp = false;
    }
}
