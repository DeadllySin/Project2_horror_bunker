using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInput;
    [SerializeField] private string verticalInput;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float dashSpeedTimeAtMaxSpeed;
    [SerializeField] private float dashSpeedTimeToMaxSpeed;
    [SerializeField] private float dashSpeedTimeToDrop;
    [SerializeField] private float dashSpeedIncrease;
    [SerializeField] private float dashSpeedMax;
    [SerializeField] private float dashSpeedDecrease;
    [SerializeField] private KeyCode dashKey;
    

    private CharacterController charController;


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }


    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (Input.GetKey(dashKey))
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
        movementSpeed += dashSpeedIncrease;
        yield return new WaitForSeconds(dashSpeedTimeToMaxSpeed);
        movementSpeed = dashSpeedMax;
        yield return new WaitForSeconds(dashSpeedTimeAtMaxSpeed);
        movementSpeed -= dashSpeedDecrease;
        yield return new WaitForSeconds(dashSpeedTimeToDrop);
        movementSpeed = 5;
    }
}
