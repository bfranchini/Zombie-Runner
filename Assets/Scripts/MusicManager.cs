using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;
	private AudioSource audioSource;

	void Awake()
	{
        var exisingMusicManager  = FindObjectsOfType<MusicManager>();

	    if (exisingMusicManager.Length > 1)
	    {
            Destroy(this);
	        return;
	    }            

		DontDestroyOnLoad(gameObject);
		Debug.Log("Don't destroy on load: " + name);
	}
	
	// Use this for initialization
	void Start () 
	{				
	}

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        audioSource = GetComponent<AudioSource>();
        Debug.Log(scene.buildIndex);
        LoadMusicManager(scene.buildIndex);
    }

    private void LoadMusicManager(int level)
	{
        try
        {
            Debug.Log("Playing clip: " + levelMusicChangeArray[level]);
            var thisLevelMusic = levelMusicChangeArray[level];

            if (thisLevelMusic)
            {

                if(audioSource.clip != null && audioSource.clip.name == thisLevelMusic.name)                
                    return;
                
                if (PlayerPrefsManager.HasVoumePref())
                    audioSource.volume = PlayerPrefsManager.GetMasterVolume();

                audioSource.clip = thisLevelMusic;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        catch (Exception ex)
        {

            throw;
        }
	}
	
	public void ChangeVolume(float volume)
	{
		audioSource.volume = volume;
	}

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }
}
