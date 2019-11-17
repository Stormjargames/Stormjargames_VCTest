using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class AnimationTestScript_BasicRoom : MonoBehaviour
{
    public Animator anim;
    public string TriggerName1 = "RoomEnter";
    public string TriggerName2 = "RoomEnter";
    public string TriggerName3 = "RoomEnter";
    public string TriggerName4 = "RoomEnter";
    public string TriggerName5 = "RoomEnter";
    public string TriggerName6 = "RoomEnter";
    public string TriggerName7 = "RoomEnter";
    public string TriggerName8 = "RoomEnter";
    public string TriggerName9 = "RoomEnter";
    public string TriggerName10 = "RoomEnter";

    void TriggerAnim(string s)
    {
        anim.SetTrigger(s);
    }

    [Button]
    public void TriggerAnimation1()
    {
        TriggerAnim(TriggerName1);
    }
    [Button]
    public void TriggerAnimation2()
    {
        TriggerAnim(TriggerName2);
    }
    [Button]
    public void TriggerAnimation3()
    {
        TriggerAnim(TriggerName3);
    }
    [Button]
    public void TriggerAnimation4()
    {
        TriggerAnim(TriggerName4);
    }
    [Button]
    public void TriggerAnimation5()
    {
        TriggerAnim(TriggerName5);
    }
    [Button]
    public void TriggerAnimation6()
    {
        TriggerAnim(TriggerName6);
    }
    [Button]
    public void TriggerAnimation7()
    {
        TriggerAnim(TriggerName7);
    }
    [Button]
    public void TriggerAnimation8()
    {
        TriggerAnim(TriggerName8);
    }
    [Button]
    public void TriggerAnimation9()
    {
        TriggerAnim(TriggerName9);
    }
    [Button]
    public void TriggerAnimation10()
    {
        TriggerAnim(TriggerName10);
    }
}
