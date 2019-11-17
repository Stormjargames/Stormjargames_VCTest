using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{

    //---------------------------------------

    AudioManager am;
    public string ID;
    public bool onlyOnce = false;
    bool off = false; 
    
    //---------------------------------------

    void Start()
    {
        am = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    void OnTriggerEnter()
    {
        if (!off)
        {
            am.PlayClip(ID, 0);
            if (onlyOnce) off = true;
        }
    }
}
