using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*********************************************************************************
 * class SoundManager
 * 
 * Function: Handles all audio functionality
 *********************************************************************************/
public class SoundManager : MonoBehaviour {

	public static SoundManager i;   //Static reference to SoundManager
    public float volume;            //Master volume. Range from 0 to 1

	//EndSound vars
	AudioSource target;             //Audio source to fade out
	float duration;                 //Duration remaining on fade

	List<AudioSource> clipsPlaying = new List<AudioSource>();   //Currently playing audio sources
	public AudioClip[] clips;                                   //All available audio

	// Destroys duplicate SoundManagers and initialize default values
	void Awake () {
	    if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
	    {
	        Destroy(gameObject);
	    }
	    else
	    {
	        i = this;
	        target = null;
	        duration = 0;
            volume = 0.5f;
            //DontDestroyOnLoad(gameObject);
        }
	}

	// Check if any clips are done playing, and perform necessary cleanup. Fade out any audio
    //  sources which need to be faded out. Warps audio pitch based on timescale
	void Update () {
		
        //Check for clips which are done playing then destroy them
		for (int i = 0; i < clipsPlaying.Count; i++) {
			AudioSource source = clipsPlaying [i];
			if (!source.isPlaying) {
				clipsPlaying.Remove (source);
				Destroy (source);
				i--;
			} else {
                //Warp audio pitch
				clipsPlaying [i].pitch = Mathf.Clamp(Time.timeScale, 0.5f, 1f);
			}
		}

        //Fade out an audio source
		if (target != null) {
			target.volume -= (1/duration) * Time.deltaTime;
			if (target.volume <= 0) {
				clipsPlaying.Remove (target);
				Destroy (target);
				target = null;
			}
		}
	}

    /// <summary>
    /// Play the specified clip at the specified volume (from 0 to 1)
    /// </summary>
    /// <param name="clipNumber"></param>
    /// <param name="volume"></param>
	public void PlaySound(Sound clipNumber, float volume){
		AudioSource source = gameObject.AddComponent<AudioSource> ();
		source.clip = clips [(int)clipNumber];
		source.volume = volume;
		source.Play ();
		clipsPlaying.Add (source);
	}

    /// <summary>
    /// Play the specified clip at the specified volume (from 0 to 1) looping
    /// </summary>
    /// <param name="clipNumber"></param>
    /// <param name="volume"></param>
	public void PlaySoundLoop(Sound clipNumber, float volume){
		AudioSource source = gameObject.AddComponent<AudioSource> ();
		source.clip = clips [(int)clipNumber];
		source.volume = volume;
		source.Play ();
		source.loop = true;
		clipsPlaying.Add (source);
	}

    /// <summary>
    /// End the specified clip abruptly
    /// </summary>
    /// <param name="soundName"></param>
	public void EndSoundAbrupt(string soundName){
		AudioSource[] sources = gameObject.GetComponents<AudioSource>();
		for (int i = 0; i < sources.Length; i++) {
			if (sources[i].clip.name == soundName) {
				clipsPlaying.Remove (sources [i]);
				Destroy (sources[i]);
				break;
			}
		}
	}

    /// <summary>
    /// Fade the specified clip out over duration d
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="d"></param>
	public void EndSoundFade(string soundName, float d){
		AudioSource[] sources = gameObject.GetComponents<AudioSource>();
		for (int i = 0; i < sources.Length; i++) {
			if (sources[i].clip.name == soundName) {
				target = sources[i];
				break;
			}
		}
		duration = d;
	}

    /// <summary>
    /// End all currently playing sound
    /// </summary>
    public void EndAllSound()
    {
        AudioSource[] sources = gameObject.GetComponents<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
                EndSoundAbrupt(sources[i].clip.name);
        }
    }

    /// <summary>
    /// End all currently playing sounds of the specified type
    /// </summary>
    /// <param name="soundName"></param>
    public void EndAllSound(string soundName)
    {
        AudioSource[] sources = gameObject.GetComponents<AudioSource>();
        for (int i = 0; i < sources.Length; i++)
        {
            if (sources[i].clip.name == soundName)
            {
                EndSoundAbrupt(soundName);
            }
        }
    }
}

//Sound enum
//Maps sound clips to a recognizable name
[System.Serializable]
public enum Sound{
	StoryAudio1 = 0,
    Explosion = 1,
    Hit = 2,
    Shoot = 3,
    Level1Audio = 4,
    StoryAudio2 = 5,
    StoryAudio3 = 6,
    Level2Audio = 7,
	WarpSpeed = 8,
	MainMenuAudio = 9
};
