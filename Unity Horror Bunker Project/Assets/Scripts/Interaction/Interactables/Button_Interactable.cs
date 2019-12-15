using System;
using UnityEngine;
using MyBox;

public class Button_Interactable : vp_Interactable
{
    [Space(10)]
    [Header("Custom")]

    [SerializeField]
    private string toolTipText = null;

    [SerializeField]
    private GameObject target = null;

    [SerializeField]
    private string targetValue = null;

    public override bool TryInteract(vp_PlayerEventHandler player)
    {
        if (!target)
        {
            return false;
        }

        target.GetComponent<InteractionTarget>().Interact(targetValue);

        return true;
    }

}