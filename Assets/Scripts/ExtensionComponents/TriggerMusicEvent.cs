using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMusicEvent : MonoBehaviour
{
    //---------------------------------------

    AudioManager am;
    public List<string> IDs;
    public float delay;
    public int layer = 0;
    public bool loop = true;
    //---------------------------------------

    void Start()
    {
        am = GameObject.Find("GameManager").GetComponent<AudioManager>();

        int rand = Random.Range(0, IDs.Count);
        string ID = IDs[rand];
        am.PlayBackgroundMusic(ID, delay, layer, false, false, loop);
    }
}
