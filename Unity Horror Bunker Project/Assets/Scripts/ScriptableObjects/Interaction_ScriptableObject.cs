
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interaction Scriptable Object", menuName = "Interaction Scriptable Object")]
public class Interaction_ScriptableObject : ScriptableObject
{

    private object[] interactionValues;

    [Header("Values")]

    [SerializeField]
    private float[] numberValues = null;
    [SerializeField]
    private string[] stringValues = null;
    [SerializeField]
    private bool[] boolValues = null;

    [Space(10)]

    [TextArea(15, 15)]
    public string description;

    public object[] GetInteractionValues() { return interactionValues; }

    public void OnEnable()
    {
        int valueCount = numberValues.Length + stringValues.Length + boolValues.Length;
        interactionValues = new object[valueCount];
        int index = 0;

        foreach (float item in numberValues)
        {
            interactionValues[index] = item;
            index++;
        }

        foreach (string item in stringValues)
        {
            interactionValues[index] = item;
            index++;
        }

        foreach (bool item in boolValues)
        {
            interactionValues[index] = item;
            index++;
        }

    }

}
