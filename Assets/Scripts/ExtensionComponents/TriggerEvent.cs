using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    //---------------------------------------

    EventManager em;
    public string ID;
    public bool onlyOnce = true;
    bool off = false;


    //---------------------------------------

    void Start()
    {
        em = GameObject.Find("GameManager").GetComponent<EventManager>();
    }

    void OnTriggerEnter()
    {
        if (!off)
        {
            print("trigger event");
            em.PlayEvent(ID);
            if (onlyOnce) off = true;
        }
    }
}
