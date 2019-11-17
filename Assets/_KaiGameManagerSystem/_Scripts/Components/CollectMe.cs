using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMe : MonoBehaviour {

    //---------------------------------------

    GameManager gm;
    AudioManager am;
    

    //---------------------------------------

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        am = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    void OnTriggerEnter()
    {
        if (gm.currentGameState == GameManager.GameState.GameIsPlaying)
        {
            gm.IncreaseScore();
        }

        Destroy(this.gameObject, Time.deltaTime);
    }
}
