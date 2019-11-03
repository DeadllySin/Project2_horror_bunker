using UnityEngine;

public class Stamina : MonoBehaviour
{
    [SerializeField]
    private float maxStamina = 5f;

    [SerializeField]
    private float staminaRegenerationRate = 0.5f;

    private bool regenerateStamina = true;
    private float currentStamina;

    private void Start()
    {
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    private void Update()
    {
        if (regenerateStamina)
        {
            RegenStamina();
        }
    }

    private void RegenStamina()
    {
        currentStamina = Mathf.Clamp(currentStamina + staminaRegenerationRate * Time.deltaTime, 0, maxStamina);
    }

    public bool UseStamina(float stamina)
    {
        currentStamina -= stamina;

        if (currentStamina >= 0f)
        {
            return true;
        }
        else
        {
            currentStamina = 0;
            return false;
        }
    }

    public float GetStamina()
    {
        return currentStamina;
    }

    public void StartRegeneration()
    {
        regenerateStamina = true;
    }

    public void StopRegeneration()
    {
        regenerateStamina = false;
    }
}