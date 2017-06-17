using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class ScreenShake
 * 
 * Function: Controls all screen shake. Must be attached to a stationary main camera
 *********************************************************************************/
public class ScreenShake : MonoBehaviour {
	//Attach this script to a stationary main camera

	public static ScreenShake i;    //Static reference
	public int lerpSpeed;           //Determines how quickly the camera resets position
	Vector3 intensity;	            //Intensity of the screen shake
	Vector3 startLocation;          //Initial location of the camera
	float timer;                    //Duration of screen shake

	// Get static reference
	void Start () {
		i = this;
	}
	
	// Perform screen shake and lerp to starting position
	void Update () {
		if (timer > 0) {
			Shake ();
			Vector3.Lerp (gameObject.transform.position, startLocation, lerpSpeed);
		}
	}

    /// <summary>
    /// Begin screen shake
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="intensity_"></param>
	public void StartShake(float duration, Vector3 intensity_){
		timer = duration;
		intensity = intensity_;
		startLocation = gameObject.transform.position;
	}

    /// <summary>
    /// Continue screen shake
    /// </summary>
	public void Shake(){
		gameObject.transform.position = startLocation + Random.Range (-1f, 1f) * intensity;
		timer -= Time.deltaTime;
	}

    /// <summary>
    /// End the screen shake effect
    /// </summary>
	public void EndShake(){
		timer = 0;
	}
}
