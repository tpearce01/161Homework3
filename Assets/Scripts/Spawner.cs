using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public static Spawner i;								    //Static reference

	public GameObject[] prefabs;								//List of all prefabs that may be instantiated
	List<GameObject> activeObjects = new List<GameObject>();	//All active objects controlled by this script

	void Start(){
		i = this;
	}

	void Update(){
		//Remove any objects that have been deleted
		activeObjects.RemoveAll(item => item == null);
	}

	//Instantiate an object at the specified location and add it to the list of active objects
	public void SpawnObject(int index, Vector3 location){
		activeObjects.Add(Instantiate (prefabs [index], location, Quaternion.identity) as GameObject);
	}
	public void SpawnObject(Prefab obj, Vector3 location){
		SpawnObject((int)obj, location);
	}

    //Instantiate an object at position with rotation
    public void SpawnObjectWithRotation(int index, Vector3 location, Vector3 rotation)
    {
        activeObjects.Add(Instantiate(prefabs[index], location, Quaternion.identity) as GameObject);
        activeObjects[activeObjects.Count - 1].transform.Rotate(rotation);
    }
    public void SpawnObjectWithRotation(Prefab obj, Vector3 location, Vector3 rotation)
    {
        SpawnObjectWithRotation((int)obj, location, rotation);
    }

    //Spawn a bullet with modified speed
    public void SpawnModifiedBullet(Prefab obj, Vector3 location, int speed)
    {
        SpawnModifiedBullet((int) obj, location, speed);
    }
    public void SpawnModifiedBullet(int index, Vector3 location, int speed)
    {
        activeObjects.Add(Instantiate(prefabs[index], location, Quaternion.identity) as GameObject);
        activeObjects[activeObjects.Count - 1].GetComponent<DefaultProjectile>().speed = speed;
    }

    //Spawn a rotated bullet with modified speed
    public void SpawnModifiedBulletRotated(Prefab obj, Vector3 location, int speed, Vector3 rotation)
    {
        SpawnModifiedBulletRotated((int)obj, location, speed, rotation);
    }
    public void SpawnModifiedBulletRotated(int index, Vector3 location, int speed, Vector3 rotation)
    {
        activeObjects.Add(Instantiate(prefabs[index], location, Quaternion.identity) as GameObject);
        activeObjects[activeObjects.Count - 1].GetComponent<DefaultProjectile>().speed = speed;
        activeObjects[activeObjects.Count - 1].transform.Rotate(rotation);
    }
}
	
//Enum to easily convert prefab names to the appropriate index
public enum Prefab{
	Shot1 = 0
};
