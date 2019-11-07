using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 3f;

    [SerializeField]
    private float runSpeed = 6f;

    [SerializeField]
    private float runSpeedRampTime = 1f;

    [SerializeField]
    private KeyCode runKey = KeyCode.LeftShift;

    private bool running;
    private float currentSpeed;
    private float currentSpeedRampTime;
    private CharacterController characterController;
    private InputManager myInputManager;
    private Stamina stamina;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        myInputManager = GameObject.FindObjectOfType<InputManager>();
        stamina = GetComponent<Stamina>();
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        if (myInputManager.PlayerCanMove() == false)
        {
            return;
        }

        CalculateSpeed();
        DoMovement();
    }

    private void CalculateSpeed()
    {
        var runKeyPressed = Input.GetKey(runKey);

        if (stamina.GetStamina() <= 0)
        {
            runKeyPressed = false;
        }

        switch (runKeyPressed)
        {
            case false when !running:
                break;

            case false when running:
                running = false;
                currentSpeed = walkSpeed;
                break;

            case true when !running:
                currentSpeedRampTime = 0f;
                running = true;
                break;

            case true when running:
                currentSpeed = Mathf.Lerp(walkSpeed, runSpeed, currentSpeedRampTime / runSpeedRampTime);
                currentSpeedRampTime += Time.deltaTime;
                break;
        }
    }

    private void DoMovement()
    {
        var walk = Input.GetAxis("Vertical") * transform.forward;
        var strafe = Input.GetAxis("Horizontal") * transform.right;
        var movement = Vector3.Normalize(strafe + walk) * currentSpeed;

        if (movement.magnitude > 0 && running)
        {
            stamina.UseStamina(Time.deltaTime);
            stamina.StopRegeneration();
        }
        else
        {
            stamina.StartRegeneration();
        }

        characterController.SimpleMove(movement);
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}