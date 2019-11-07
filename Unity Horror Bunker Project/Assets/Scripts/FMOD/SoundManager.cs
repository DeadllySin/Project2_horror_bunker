using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sm;

    [Header("FMOD Events")]
    [FMODUnity.EventRef]
    public string TriggerTest;
    [FMODUnity.EventRef]
    public string TriggerTest2;


    void Awake()
    {
        if (sm != null)
        {
            Destroy(this);
        }
        sm = this;
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
