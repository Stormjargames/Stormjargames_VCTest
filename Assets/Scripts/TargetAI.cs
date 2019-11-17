using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAI : MonoBehaviour
{
    public List<GameObject> PossibleLocations;
    public float DisappearTime = 10f;
    int storeLastLocation = 0;
    bool cont = true;
    AudioManager am;

    public void StartAI()
    {
        am = GameObject.Find("GameManager").GetComponent<AudioManager>();
        StartCoroutine(iMoveAround());
    }

    public void StopAI()
    {
        cont = false;
        StopCoroutine(iMoveAround());
    }

    public IEnumerator iMoveAround()
    {
        while(cont)
        {
            int rand = Random.Range(0, PossibleLocations.Count);
            if(rand == storeLastLocation)
            {
                if(rand+1 >= PossibleLocations.Count)
                {
                    rand--;
                }
                else
                {
                    rand++;
                }
            }
            storeLastLocation = rand;

            Vector3 randPos = PossibleLocations[rand].transform.position;
            this.transform.position = new Vector3(randPos.x, transform.position.y, randPos.z);
            am.PlayClip("BellToll", 0);
            yield return new WaitForSeconds(DisappearTime);


        }
    }
}
