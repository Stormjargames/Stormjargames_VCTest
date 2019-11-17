using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //---------------------------------------

    public enum GameState
    {
        GameStart,
        GameIsPlaying,
        GameEnd,
        ChangeScene,
        AppClose
    }

    public GameState currentGameState = GameState.GameStart;
    UIManager uimgr;
    public int Score = 0;
    public float TimeBetweenDestroySunAndEnd = 10;
    public int currentSceneID;
    public int nextSceneID
    {
        get
        {
            if (currentSceneID < SceneManager.sceneCountInBuildSettings-1)
            {
                return currentSceneID + 1;
            }
            else
            {
                return 0;
            }
        }
    }

    //---------------------------------------

    void Start()
    {
        currentSceneID = SceneManager.GetActiveScene().buildIndex;
        uimgr = GetComponent<UIManager>();
    }

	void Update ()
    {
        switch(currentGameState)
        {
            case GameState.GameStart:
                GameStart();
                break;
            case GameState.GameIsPlaying:
                GameIsPlaying();
                break;
            case GameState.GameEnd:
                GameEnd();
                break;
            case GameState.ChangeScene:
                NextScene();
                break;
            case GameState.AppClose:
                AppClose();
                break;
        }	
	}

    public void GameStart()
    {
        //turn on UI
        uimgr.UIInit();
        currentGameState = GameState.GameIsPlaying;
    }

    public void GameIsPlaying()
    {
        //update the UI
        uimgr.UIUpdate(Score);
    }

    public void GameEnd()
    {
        //turn off UI
        currentGameState = GameState.AppClose;
        uimgr.UIEnd(TimeBetweenDestroySunAndEnd);
        Invoke("QuitApp", TimeBetweenDestroySunAndEnd);
    }

    public void NextScene()
    {
        //turn off UI
        currentGameState = GameState.AppClose;
        uimgr.UIEnd(TimeBetweenDestroySunAndEnd);
        Invoke("LoadScene", TimeBetweenDestroySunAndEnd);
    }

    public void AppClose()
    {
        uimgr.UIUpdate();
    }

    void QuitApp()
    {
        Application.Quit();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(nextSceneID);
    }

    public void IncreaseScore()
    {
        Score++;
    }
}
