using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;

	void Awake () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
		}
	}

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        music.Stop();

        if (scene.buildIndex == 0)
        {
            music.clip = startClip;
        }
        if (scene.buildIndex == 1)
        {
            music.clip = gameClip;
        }
        if (scene.buildIndex == 2)
        {
            music.clip = endClip;
        }

        music.loop = true;
        music.Play();
    }

    /*    void OnLevelWasLoaded(int level)
    {
        music.Stop();

        if(level == 0)
        {
            music.clip = startClip;
        }
        if (level == 1)
        {
            music.clip = gameClip;
        }
        if (level == 2)
        {
            music.clip = endClip;
        }

        music.loop = true;
        music.Play();
    }*/
}
