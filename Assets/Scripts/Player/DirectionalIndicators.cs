using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalIndicators : MonoBehaviour {

	private Vector3 finalPos;           //Final position to move player to
	public GameObject indicator;
	public int controllerNumber;

	//Get player input and move player is applicable
	public void Update()
	{
		finalPos = gameObject.transform.position;
		ControllerInput();
		// KEYBOARD CONTROLS FOR TESTING
		if (finalPos == gameObject.transform.position)
		{
			KeyboardInput();
		}
		if (finalPos == gameObject.transform.position) {
			indicator.SetActive (false);
		} else {
			indicator.SetActive (true);
			gameObject.transform.right = finalPos - gameObject.transform.position;
		}
	}

	//KEYBOARD CONTROLS FOR TESTING ONLY
	void KeyboardInput()
	{
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
		{
			finalPos.y += 2;
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			finalPos.x -= 2;
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
		{
			finalPos.y -= 2;
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			finalPos.x += 2;
		}
	}

	void ControllerInput()
	{
		Vector2 ls = new Vector2(Input.GetAxis("LSX" + controllerNumber), Input.GetAxis("LSY" + controllerNumber));
		if (ls.y > .2f)
		{
			finalPos.y -= 10 * ls.y;
		}
		if (ls.y < -.2f)
		{
			finalPos.y += 10 * -ls.y;
		}
		if (ls.x < -.2f)
		{
			finalPos.x -= 10 * -ls.x;
		}
		if (ls.x > .2f)
		{
			finalPos.x += 10 * ls.x;
		}
	}
}
