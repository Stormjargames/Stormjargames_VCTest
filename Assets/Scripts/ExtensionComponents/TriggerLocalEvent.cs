using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLocalEvent : MonoBehaviour
{
    //---------------------------------------

    EventManager em;
    public SystemEvent thisSystemEvent;
    public float delay = 0f;

    //---------------------------------------

    void Start()
    {
        em = GameObject.Find("GameManager").GetComponent<EventManager>();
        thisSystemEvent.objectToTrigger = this.gameObject;
        em.PlayLocalEvent(thisSystemEvent, delay);
    }



}
