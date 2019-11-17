using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextBoxContent
{
    //---------------------------------------

    public string ID;
    [TextArea]
    public string content;

    //---------------------------------------
}


public class ScriptManager : MonoBehaviour {

    //---------------------------------------

    [SerializeField]
    public List<TextBoxContent> MasterScript;

    //---------------------------------------
}
