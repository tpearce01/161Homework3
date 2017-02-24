using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

	public static Parallax i;

	public Sprite[] sources;
	List<GameObject> backgrounds = new List<GameObject>();
	List<float> originalPosition = new List<float>();
	public float spriteWidth;
	public int minSpeed;
	public int maxSpeed;

	void Awake(){
		i = this;
	}

	void Start(){
		CreateSprites ();
	}

	void Update(){
		MoveSprites ();
	}

	void CreateSprites(){
		for (int i = 0; i < sources.Length; i++) {
			backgrounds.Add (new GameObject ());
			backgrounds [i].AddComponent<SpriteRenderer> ();
			backgrounds [i].AddComponent<BoxCollider2D> ();
			backgrounds [i].GetComponent<BoxCollider2D> ().isTrigger = true;
			backgrounds [i].GetComponent<SpriteRenderer> ().sprite = sources [i];
			backgrounds [i].transform.position = new Vector3 (0, 0, 1 + (i/10f));
			backgrounds [i].transform.localScale = new Vector3 (2, 1, 1);
			backgrounds [i].tag = "Background";
		}
		for (int i = 0; i < sources.Length; i++) {
			backgrounds.Add (new GameObject ());
			backgrounds [i + sources.Length].AddComponent<SpriteRenderer> ();
			backgrounds [i + sources.Length].AddComponent<BoxCollider2D> ();
			backgrounds [i + sources.Length].GetComponent<BoxCollider2D> ().isTrigger = true;
			backgrounds [i + sources.Length].GetComponent<SpriteRenderer> ().sprite = sources [i];
			backgrounds [i + sources.Length].transform.position = new Vector3 (spriteWidth, 0, 1 + (i/10f));
			backgrounds [i + sources.Length].transform.localScale = new Vector3 (2, 1, 1);
			backgrounds [i + sources.Length].tag = "Background";
			backgrounds [i + sources.Length].transform.rotation = new Quaternion(0,180,0, 0);
		}
	}

	void MoveSprites(){
		for (int i = 0; i < backgrounds.Count; i++) {
			backgrounds [i].transform.position -= transform.right * Mathf.Lerp(minSpeed, maxSpeed, (float)(6 - i%6) / backgrounds.Count) * Time.deltaTime;
		}
	}
}
