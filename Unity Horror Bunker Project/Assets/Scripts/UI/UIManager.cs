using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Image crosshair = null;
    
    [SerializeField]
    private Image crosshairInteract = null;
        
    // Start is called before the first frame update
    void Start()
    {
        CrossHair(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CrossHair(bool showCrosshair)
    {
        crosshair.enabled = showCrosshair;
    }

    public void CrosshairInteract(bool interactCrosshair)
    {
        crosshairInteract.enabled = interactCrosshair;
    }
}
