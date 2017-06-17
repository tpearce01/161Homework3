using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class DirectionalIndicators
 * 
 * Function: DEPRECATED CLASS. This class is meant for multiplayer controls, and
 *      thus is not active in this version. Controls directional indicators
 *      on players to improve visual coordination among players
 *********************************************************************************/
public class DirectionalIndicators : MonoBehaviour {

	private Vector3 finalPos;           //Final position to point arrow at
	public GameObject indicator;        //Arrow indicator object
	public int controllerNumber;        //Controller number attached to this indicator

	//Get player input and point indicator at the specified direction
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

	/// <summary>
    /// Keyboard controls for testing only. Gets keyboard input
    /// </summary>
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

    /// <summary>
    /// Get controller input for movement
    /// </summary>
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
