using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour {

    //---------------------------------------

    public GameObject PopupUIElement;
    CanvasGroup PopupCanvasGroup;
    float PopupAlpha = 0;
    Text PopupTextBox;

    public GameObject CounterUIElement;
    float CounterAlpha = 0;
    CanvasGroup CounterCanvasGroup;
    Text CounterTextBox;

    public GameObject WhiteOutUIElement;
    float WhiteOutAlpha = 0;
    CanvasGroup WhiteOutCanvasGroup;

    ScriptManager sm;

    public float PopupLerpSpeedUp = 0.5f;
    public float PopupLerpSpeedDown = 0.5f;

    public float CounterLerpSpeedUp = 0.5f;
    public float CounterLerpSpeedDown = 0.5f;

    public float WhiteOutLerpSpeedUp = 0.5f;
    public float WhiteOutLerpSpeedDown = 0.5f;

    //---------------------------------------

    public void UIInit()
    {
        PopupCanvasGroup = PopupUIElement.GetComponent<CanvasGroup>();
        PopupAlpha = 0;
        PopupCanvasGroup.alpha = PopupAlpha;
        PopupTextBox = PopupUIElement.transform.GetChild(0).GetComponent<Text>();

        CounterCanvasGroup = CounterUIElement.GetComponent<CanvasGroup>();
        CounterAlpha = 0;
        CounterCanvasGroup.alpha = CounterAlpha;
        CounterTextBox = CounterUIElement.transform.GetChild(0).GetComponent<Text>();

        WhiteOutCanvasGroup = WhiteOutUIElement.GetComponent<CanvasGroup>();
        WhiteOutAlpha = 1; // set to 1 for whiteout to lerp from 1
        WhiteOutCanvasGroup.alpha = WhiteOutAlpha;

        sm = GetComponent<ScriptManager>();

        StartCoroutine(CounterLerp(CounterAlpha, 1, CounterLerpSpeedUp));

        StartCoroutine(WhiteOutLerp(WhiteOutAlpha, 0, WhiteOutLerpSpeedDown)); // add this to start the lerp


    }


    public IEnumerator PopupLerp(float initialValue, float finalValue, float speed)
    {
        float tParam = 0;
        float valToBeLerped = 0;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed;
            valToBeLerped = Mathf.Lerp(initialValue, finalValue, tParam);
            PopupAlpha = valToBeLerped;
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }

        PopupAlpha = finalValue;

        //Debug.Log("Lerp success a!");
        yield return null;
    }

    public IEnumerator CounterLerp(float initialValue, float finalValue, float speed)
    {
        Debug.Log("Whiteout");
        float tParam = 0;
        float valToBeLerped = 0;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed;
            valToBeLerped = Mathf.Lerp(initialValue, finalValue, tParam);
            CounterAlpha = valToBeLerped;
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }

        CounterAlpha = finalValue;

        //Debug.Log("Lerp success a!");
        yield return null;
    }

    public IEnumerator WhiteOutLerp(float initialValue, float finalValue, float speed)
    {
        float tParam = 0;
        float valToBeLerped = 0;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed;
            valToBeLerped = Mathf.Lerp(initialValue, finalValue, tParam); 
            WhiteOutAlpha = valToBeLerped;
            yield return new WaitForSeconds(Time.deltaTime * speed);
        }

        WhiteOutAlpha = finalValue;

       // Debug.Log("Lerp success a!");
        yield return null;
    }

    public void UIUpdate(int currentScore)
    {
        PopupCanvasGroup.alpha = PopupAlpha;
        CounterCanvasGroup.alpha = CounterAlpha;
        WhiteOutCanvasGroup.alpha = WhiteOutAlpha;

        CounterTextBox.text = currentScore.ToString();
    }

    public void UIUpdate()
    {
        PopupCanvasGroup.alpha = PopupAlpha;
        CounterCanvasGroup.alpha = CounterAlpha;
        WhiteOutCanvasGroup.alpha = WhiteOutAlpha;
    }

    public void UIShowMessage(string ID)
    {
        List<TextBoxContent> queryOnMasterScript = new List<TextBoxContent>();
        queryOnMasterScript = sm.MasterScript.Where(o => o.ID == ID).ToList();

        if (queryOnMasterScript.Count != 1)
        {
            Debug.LogError("script ID incorrect - please check");
            return;
        }
        PopupTextBox.text = queryOnMasterScript[0].content;

        //PopupCanvasGroup.alpha = 1; // not smooth yet
        StopCoroutine("PopupLerp");
        StartCoroutine(PopupLerp(PopupAlpha, 1, PopupLerpSpeedUp));

    }

    public void UIHideMessage()
    {
        //PopupCanvasGroup.alpha = 0; // not smooth yet
        StopCoroutine("PopupLerp");
        StartCoroutine(PopupLerp(PopupAlpha, 0, PopupLerpSpeedDown));
        //Debug.Log("Hide Message");
    }

    public void UIEnd(float timeToLerp)
    {
        StartCoroutine(PopupLerp(PopupAlpha, 0, PopupLerpSpeedDown));

        StartCoroutine(CounterLerp(CounterAlpha, 0, CounterLerpSpeedDown));
        StartCoroutine(WhiteOutLerp(WhiteOutAlpha, 1, WhiteOutLerpSpeedUp));

        //print("here");
    }

    public void UIShowMessage(string ID, float stayingTime)
    {
        UIShowMessage(ID);
        Invoke("UIHideMessage", stayingTime);
    }
}
