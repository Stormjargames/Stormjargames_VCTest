using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSFXEvent : MonoBehaviour
{
    //---------------------------------------

    AudioManager am;
    public List<string> IDs;
    public float delay;
    //---------------------------------------

    void Start()
    {
        am = GameObject.Find("GameManager").GetComponent<AudioManager>();

        int rand = Random.Range(0, IDs.Count);
        string ID = IDs[rand];
        am.PlayClip(ID, delay);
    }
}
