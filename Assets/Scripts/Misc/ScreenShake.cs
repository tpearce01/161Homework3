using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {
	//Attach this script to a stationary main camera

	public static ScreenShake i;
	public int lerpSpeed;
	Vector3 intensity;	
	Vector3 startLocation;
	float timer;

	// Use this for initialization
	void Start () {
		i = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0) {
			Shake ();
			Vector3.Lerp (gameObject.transform.position, startLocation, lerpSpeed);
		}
	}

	public void StartShake(float duration, Vector3 intensity_){
		timer = duration;
		intensity = intensity_;
		startLocation = gameObject.transform.position;
	}

	public void Shake(){
		gameObject.transform.position = startLocation + Random.Range (-1f, 1f) * intensity;
		timer -= Time.deltaTime;
	}

	public void EndShake(){
		timer = 0;
	}
}
