using UnityEngine;
using System.Collections;

public class Keypad : MonoBehaviour
{

    public string curPassword = "12345";
    public string input;
    public bool onTrigger; //checks if the player collides with the collider
    public bool doorOpen; //checks if the door is opened
    public Transform doorHinge;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject EnterCodeButton;
    [SerializeField] private GameObject CorrectCodeLight, ErrorCodeLight;
    [SerializeField] private Material CorrectCodeLightOnMaterial, ErrorCodeLightOnMaterial;
    [SerializeField] private TextMesh CodeText;
    


    void OnTriggerEnter(Collider other)
    {
        onTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        onTrigger = false;
        input = "";
    }

    void Update()
    {
        if (!doorOpen && Input.GetMouseButtonDown(0))        //fix enter text pressed bug
            OnButtonClicked();
        if (!doorOpen && Input.GetMouseButtonDown(0)) ;
        {
            OnEnterTextButton();
        }      

        if (doorOpen)
        {
                var newRot = Quaternion.RotateTowards(doorHinge.rotation, Quaternion.Euler(0.0f, -90.0f, 0.0f), Time.deltaTime * 25);
                doorHinge.rotation = newRot;
        }
    }

    void OnGUI()
    {
        if (!doorOpen)
        {
            if (onTrigger)
            {
                GUI.Box(new Rect(0, 0, 400, 50), "TIP: Press 'E' to open keypad"); //displays the "Tip" GUI to let the player know what to do

               
            }
        }
    }

    void OnButtonClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2.0f))
        {
            if (hit.transform != null)
            { 
                for (int i = 0; i < 11; i++)
                {
                    if (hit.transform.gameObject == buttons[i])
                    {
                        input += i.ToString();
                        CodeText.GetComponent<TextMesh>().text = input;
                        //play button pressed sound
                    }
                }
            }
        }
    }

    void OnEnterTextButton()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            if (hit.transform != null)
            {

                if (hit.transform.gameObject == EnterCodeButton)
                {
                    
                    //play pressed button sound
                    if (input == curPassword)
                    {
                        CorrectCodeLight.GetComponent<MeshRenderer>().material = CorrectCodeLightOnMaterial;
                        StartCoroutine(DisplayAuthorizedText());
                    }
                    else if (input != curPassword)
                    {
                        ErrorCodeLight.GetComponent<MeshRenderer>().material = ErrorCodeLightOnMaterial;
                        StartCoroutine(DisplayErrorText());
                    }
                }
            }
        }
    }

    IEnumerator DisplayErrorText()
    {
        yield return new WaitForSeconds(0.5f);
        CodeText.GetComponent<TextMesh>().text = "DENIED";
        //play error sound
    }
    
    IEnumerator DisplayAuthorizedText()
    {
        yield return new WaitForSeconds(0.5f);
        CodeText.GetComponent<TextMesh>().text = "AUTHORIZED";
        doorOpen = true;
        //play authorized sound
    }
}
