using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*********************************************************************************
 * class DestroyAfterTime
 * 
 * Function: Destroys an object after a set amount of time
 *********************************************************************************/
public class DestroyAfterTime : MonoBehaviour
{
    public float timer; //Time until destruction

    //Begin destruction countdown
	void Start ()
	{
	    Destroy(gameObject, timer);
	}
}
