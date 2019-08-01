using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
    [SerializeField]
    AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
    [SerializeField]
    AudioSource guitarSource;                 //Drag a reference to the audio source which will play the music.

    public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             

    static Dictionary<SoundName, AudioClip> audioClips =
        new Dictionary<SoundName, AudioClip>();

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
        {
            //if not, set it to this.
            instance = this;
            Initialize();
        }
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }

    void Initialize()
    {
        audioClips.Add(SoundName.MainMusic,
            Resources.Load<AudioClip>("song"));
        audioClips.Add(SoundName.GuitarMusic,
            Resources.Load<AudioClip>("guitar"));
        audioClips.Add(SoundName.ErroFx,
            Resources.Load<AudioClip>("error"));
    }

    //Used to play single sound clips.
    public void PlaySingleFx(SoundName name)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = audioClips[name];

        //Play the clip.
        efxSource.Play();
    }

    //Used to play Music.
    public void PlayMusic()
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        musicSource.clip = audioClips[SoundName.MainMusic];
        guitarSource.clip = audioClips[SoundName.GuitarMusic];
        //Play the clip.
        musicSource.Play();
        guitarSource.Play();
    }

    //Used to play Music.
    public void StopMusic()
    {
        //Play the clip.
        musicSource.Stop();
        guitarSource.Stop();
    }

    //Used to play Music.
    public void PauseMusic()
    {
        //Play the clip.
        musicSource.Pause();
        guitarSource.Pause();
    }

    //Used to play Music.
    public void UnPauseMusic()
    {
        //Play the clip.
        musicSource.UnPause();
        guitarSource.UnPause();
    }
}