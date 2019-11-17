using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushDetection : MonoBehaviour
{
    public float crushCountLimit = 1;
    GameManager gm;
    AudioManager am;
    bool crushed = false;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        am = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }

    void OnTriggerStay(Collider other)
    {
        if(!crushed)
        {
            if(other.tag == "Walls")
            {
                float count = 0;
                while(count < crushCountLimit)
                {
                    count += Time.deltaTime;
                }
                crushed = true;
                am.PlayClip("Pain", 0f);
                am.PlayBackgroundMusic("Crushed", 0.0f,2,false,false,false);

                gm.currentGameState = GameManager.GameState.ChangeScene;

            }
        }
    }
}
