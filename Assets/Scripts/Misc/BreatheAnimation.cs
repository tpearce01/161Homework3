using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BreatheAnimation : MonoBehaviour
{
    public List<GameObject> obj;

	// Update is called once per frame
	void Update ()
	{
	    BreateAnimation();
	}

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

    public void SetObjList(List<GameObject> o)
    {
        ResetObjScale();
        obj = o;
    }

    public void AddObj(GameObject o)
    {
        obj.Add(o);
    }

    public void ResetObjScale()
    {
        foreach (var o in obj)
        {
            o.transform.localScale = Vector3.one;
        }
    }

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

    public void RemoveObj(GameObject o)
    {
        obj.Remove(o);
    }
}
