using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingObject : MonoBehaviour {

    //---------------------------------------

    GameManager gm;
    UIManager ui;
    public string scriptID;
    public bool turnMessageOffAfterTime = false;
    public float messageStayTime = 1;

    //---------------------------------------

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ui = GameObject.Find("GameManager").GetComponent<UIManager>();
    }

    void OnTriggerEnter()
    {
        if(gm.currentGameState == GameManager.GameState.GameIsPlaying)
        {
            if(turnMessageOffAfterTime)
            {
                ui.UIShowMessage(scriptID, messageStayTime);
            }
            else
            {
                ui.UIShowMessage(scriptID);
            }
        }
    }

    void OnTriggerExit()
    {
        if (gm.currentGameState == GameManager.GameState.GameIsPlaying)
        {
            if (turnMessageOffAfterTime)
            {
                //ui.UIShowMessage(scriptID, messageStayTime);
            }
            else
            {
                ui.UIHideMessage();
            }
        }
    }
}
