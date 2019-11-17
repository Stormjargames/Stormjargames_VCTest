using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum SystemEventTypes
{
    Animation,
    System
}

[System.Serializable]
public class SystemEvent
{
    //---------------------------------------

    public string ID;
    public string FindGameObjectName;
    public GameObject objectToTrigger;
    public SystemEventTypes typeOfEvent;
    public string parameters1;
    public string parameters2;

    //---------------------------------------
}

public class EventManager : MonoBehaviour
{

    //---------------------------------------

    public List<SystemEvent> AllEvents;

    //---------------------------------------

    public void PlayEvent(string ID)
    {
        List<SystemEvent> queryList = AllEvents.Where(o => o.ID == ID).ToList();

        if (queryList.Count != 1)
        {
            Debug.LogError("event ID incorrect - please check");
            return;
        }

        EventAction(queryList[0], 0);
    }

    public void PlayLocalEvent(SystemEvent s, float delay)
    {
        s.ID += " localEvent";
        EventAction(s, delay);
    }
    void EventAction(SystemEvent s, float delay)
    {
        switch (s.typeOfEvent)
        {
            case SystemEventTypes.Animation:
                s.objectToTrigger.GetComponent<Animator>().SetTrigger(s.parameters1);
                break;
            case SystemEventTypes.System:
                switch (s.parameters1)
                {
                    case "FollowingSpawner":
                        FollowingSpawner f = s.objectToTrigger.GetComponent<FollowingSpawner>();
                        f.Invoke(s.parameters2, delay);
                        break;
                    case "TargetAI":
                        TargetAI tai = s.objectToTrigger.GetComponent<TargetAI>();
                        tai.Invoke(s.parameters2, delay);
                        break;
                }
                break;
        }
    }

}