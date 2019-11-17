using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]
public class SFX
{
    //---------------------------------------

    public string ID;
    public AudioClip clip;
    //---------------------------------------
}

public class AudioManager : MonoBehaviour {
    
    //---------------------------------------

    public List<SFX> AllClips;
    public List<SFX> AllMusic;
    AudioSource SFXPlayer;
    List<AudioSource> MusicLayers = new List<AudioSource>();
    int selectMusicLayer = 0;

    //---------------------------------------

    void Awake()
    {
        SFXPlayer = GetComponent<AudioSource>();
        foreach(Transform t in this.transform)
        {
            MusicLayers.Add(t.GetComponent<AudioSource>());
        }
    }

    public void PlayClip(string ID, float delay)
    {
        List<SFX> queryList = AllClips.Where(o => o.ID == ID).ToList();

        if (queryList.Count != 1)
        {
            Debug.LogError("clip ID incorrect - please check");
            return;
        }

        SFXPlayer.clip = queryList[0].clip;

        Invoke("GoPlayAudioClip", delay);
    }

    public void PlayBackgroundMusic(string ID, float delay, int layer, bool stopPrevious, bool fadeTransition, bool loop)
    {
        List<SFX> queryList = AllMusic.Where(o => o.ID == ID).ToList();

        if (queryList.Count != 1)
        {
            Debug.LogError("music ID incorrect - please check");
            return;
        }

        //layering, stop previous, fade transition, loop TODO

        selectMusicLayer = layer;
        MusicLayers[selectMusicLayer].clip = queryList[0].clip;
        MusicLayers[selectMusicLayer].loop = loop;

        Invoke("GoPlayMusic", delay);
    }

    void GoPlayAudioClip()
    {
        SFXPlayer.PlayOneShot(SFXPlayer.clip);
    }

    void GoPlayMusic()
    {
        MusicLayers[selectMusicLayer].Play();
    }
}


