using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class Parallax
 * 
 * Function: Controls the movement of a parallax background. Images are mirrored
 *      to ensure seamless images.
 *********************************************************************************/
public class Parallax : MonoBehaviour {

	public static Parallax i;                               //Static reference
	public Sprite[] sources;                                //Source images
	List<GameObject> backgrounds = new List<GameObject>();  //Background objects to display source images
	public Vector3 originalPosition;                        //Starting position
	public float spriteWidth;                               //Width of the images
	public float minSpeed;                                  //Move speed of front image
	public float maxSpeed;                                  //Move speed of last image

    //Get static reference
	void Awake(){
		i = this;
	}

    //Create background objects and apply the sprite images
	void Start(){
		CreateSprites ();
	}

    //Handle background movement
	void FixedUpdate(){
		MoveSprites ();
	}

    /// <summary>
    /// Generate background objects based on the number of source images. Mirrors
    /// a second set of objects to ensure seamless images
    /// </summary>
	void CreateSprites(){
        //Generate background object
		for (int i = 0; i < sources.Length; i++) {
			backgrounds.Add (new GameObject ());
			backgrounds [i].AddComponent<SpriteRenderer> ();
			backgrounds [i].GetComponent<SpriteRenderer> ().sprite = sources [i];
            backgrounds[i].AddComponent<BoxCollider2D>();
            backgrounds[i].GetComponent<BoxCollider2D>().isTrigger = true;
            backgrounds [i].transform.position = new Vector3 (0, 0, 1 + (i/10f));
			backgrounds [i].transform.localScale = new Vector3 (2, 0.8f, 1);
			backgrounds [i].tag = "Background";
		}
        //Generate mirrored background object
		for (int i = 0; i < sources.Length; i++) {
			backgrounds.Add (new GameObject ());
			backgrounds [i + sources.Length].AddComponent<SpriteRenderer> ();
			backgrounds [i + sources.Length].GetComponent<SpriteRenderer> ().sprite = sources [i];
            backgrounds[i + sources.Length].AddComponent<BoxCollider2D>();
            backgrounds[i + sources.Length].GetComponent<BoxCollider2D>().isTrigger = true;
            backgrounds [i + sources.Length].transform.position = new Vector3 (spriteWidth, 0, 1 + (i/10f));
			backgrounds [i + sources.Length].transform.localScale = new Vector3 (2, 0.8f, 1);
			backgrounds [i + sources.Length].tag = "Background";
			backgrounds [i + sources.Length].transform.rotation = new Quaternion(0,180,0, 0);
		}
	}

    /// <summary>
    /// Moves all of the background objects based on their positions
    /// </summary>
	void MoveSprites(){
		for (int i = 0; i < backgrounds.Count; i++) {
			backgrounds [i].transform.position -= transform.right * (minSpeed + (5-i%6)*maxSpeed*maxSpeed) * Time.deltaTime;
		}
	}
}
