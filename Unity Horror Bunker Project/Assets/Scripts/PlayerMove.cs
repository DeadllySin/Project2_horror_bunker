using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;//Horizontal
    [SerializeField] private string verticalInputName;//Vertical

    [SerializeField] private float walkSpeed, runSpeed, slowWalkingSpeed;
    [SerializeField] private float runBuildUpSpeed;//Acceleration
    [SerializeField] private KeyCode runKey;
    [SerializeField] private KeyCode slowWalkingKey;

    private float movementSpeed;

    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

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
        float horizInput = Input.GetAxis(horizontalInputName); //Gets the Horizontal Input(-1,1)
        float vertInput = Input.GetAxis(verticalInputName); //Gets the Vertical Input(-1,1)

        Vector3 forwardMovement = transform.forward * vertInput; //Gets the Player moving forwards and backwards
        Vector3 rightMovement = transform.right * horizInput; //Gets the Player moving left and right


        charController.SimpleMove(Vector3.ClampMagnitude(forwardMovement + rightMovement, 1.0f) * movementSpeed); //Moves the player

        if ((vertInput != 0 || horizInput != 0) && OnSlope()) // fixes movement on slope terrains or platforms
            charController.Move(Vector3.down * charController.height / 2 * slopeForce * Time.deltaTime);


        SetMovementSpeed();
    }

    private void SetMovementSpeed() // Changes between running, walking or shifting
    {
        if (Input.GetKey(runKey))
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUpSpeed);
        else if (Input.GetKey(slowWalkingKey))
            movementSpeed = Mathf.Lerp(movementSpeed, slowWalkingSpeed, Time.deltaTime * runBuildUpSpeed);
        else
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runBuildUpSpeed);
    }


    private bool OnSlope() 
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, charController.height / 2 * slopeForceRayLength))
            if (hit.normal != Vector3.up)
            {
                print("OnSlope");
                return true;
            }

        return false;
    }

}
