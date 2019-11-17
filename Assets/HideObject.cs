using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{

    private bool off = false;
    public bool onlyOnce = true;
    public float delay = 0;

    void OnTriggerEnter(Collider other)
    {
        if (!off)
        {
            Invoke("TurnGameObjectOff", delay);
            if (onlyOnce) off = true;
        }
    }

    void TurnGameObjectOff()
    {
        this.gameObject.SetActive(false);
    }
}
