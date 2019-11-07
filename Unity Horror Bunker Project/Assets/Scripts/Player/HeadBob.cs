using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField]
    private bool headBobbing = true;

    [SerializeField]
    private float bobbingSpeed = 0.2f;

    [SerializeField]
    private float bobbingAmount = 0.05f;

    [SerializeField]
    private float midpoint = 0.6f;

    [SerializeField]
    private PlayerController playerController = null;

    private InputManager myInputManager;

    private float bobSpeedFactor = 3.0f;
    private float timer = 0.0f;

    private void Start()
    {
        myInputManager = GameObject.FindObjectOfType<InputManager>();
    }

    private void FixedUpdate()
    {
        if (!headBobbing && myInputManager.PlayerCanMove())
        {
            return;
        }

        var currentBobbingSpeed = playerController.GetCurrentSpeed() * bobbingSpeed / bobSpeedFactor;
        
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cSharpConversion = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer = timer + currentBobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }
        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            cSharpConversion.y = midpoint + translateChange;
        }
        else
        {
            cSharpConversion.y = midpoint;
        }

        transform.localPosition = cSharpConversion;
    }
}