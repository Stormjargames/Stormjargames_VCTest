using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndObject : MonoBehaviour {

    //---------------------------------------

    GameManager gm;

    //---------------------------------------

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

	void OnTriggerEnter()
    {
        gm.currentGameState = GameManager.GameState.GameEnd;
    }
}
