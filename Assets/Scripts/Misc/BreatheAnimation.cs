using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

/*********************************************************************************
 * class Spawner
 * 
 * Function: Handles the spawning of all obejcts
 *********************************************************************************/
public class BreatheAnimation : MonoBehaviour
{
    public List<GameObject> obj;    //List of objects to perform "breathing" animation on

	// Update the animation
	void Update ()
	{
	    BreateAnimation();
	}

    /// <summary>
    /// Change objects size based on time to create a "breathing" effect
    /// </summary>
    public void BreateAnimation()
    {
        if (obj.Count > 0)
        {
            foreach (var t in obj)
            {
                if ((int) Time.time%2 == 0)
                {
                    t.transform.localScale *= 1.001f;
                }
                else
                {
                    t.transform.localScale *= 0.999f;
                }
            }
        }
    }

    /// <summary>
    /// Set a list of objects to begin "breathing"
    /// </summary>
    /// <param name="o"></param>
    public void SetObjList(List<GameObject> o)
    {
        ResetObjScale();
        obj = o;
    }

    /// <summary>
    /// Add a single object to begin "breathing"
    /// </summary>
    /// <param name="o"></param>
    public void AddObj(GameObject o)
    {
        obj.Add(o);
    }

    /// <summary>
    /// Reset all object scales
    /// </summary>
    public void ResetObjScale()
    {
        foreach (var o in obj)
        {
            o.transform.localScale = Vector3.one;
        }
    }

    /// <summary>
    /// Reset a single object's scale
    /// </summary>
    /// <param name="o"></param>
    public void ResetObjScale(GameObject o)
    {

        for (int j = 0; j < obj.Count; j++)
        {
            if (obj[j] == o)
            {
                obj[j].transform.localScale = new Vector3(1,1,1);
            }
        }
    }

    /// <summary>
    /// Remove a single object
    /// </summary>
    /// <param name="o"></param>
    public void RemoveObj(GameObject o)
    {
        obj.Remove(o);
    }
}
