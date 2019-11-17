using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerText : MonoBehaviour
{
    //---------------------------------------

    GameManager gm;
    UIManager ui;
    public string scriptID;
    public float messageStayTime = 1;

    //---------------------------------------

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        ui = GameObject.Find("GameManager").GetComponent<UIManager>();

        if (gm.currentGameState == GameManager.GameState.GameIsPlaying)
        {
            ui.UIShowMessage(scriptID,messageStayTime);
        }
    }

        
}
