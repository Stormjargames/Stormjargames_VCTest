using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    //---------------------------------------

    AudioManager am;
    public List<string> IDs;
    public float delay;
    public int layer = 0;
    public bool loop = true;
    public bool onlyOnce = true;
    bool off = false;
    //---------------------------------------

    void Start()
    {
        am = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(!off)
        {
            int rand = Random.Range(0, IDs.Count);
            string ID = IDs[rand];
            am.PlayBackgroundMusic(ID, delay, layer, false, false, loop);
            if (onlyOnce) off = true;
        }
    }
}
