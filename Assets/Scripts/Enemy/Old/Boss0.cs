using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss0 : MonoBehaviour
{
    public float bossCurrentHealth =100f;
    public float bossMaxHealth =100f;
    public int phase = 0;
    public int speed = 3;
   
	// Update is called once per frame
	//void Update ()
	//{
	//    //Move();
	//    //switch (phase)
	//    //{
 //    //       case 0:
 //    //           CheckPhase();
	//    //        break;
 //    //       case 1:
 //    //           FinalPhase();
	//    //        break;
 //    //       default:
 //    //           CheckDestroy();
	//    //        break;
	//    //}
	//}

    public virtual void  Move(int bossSpeed)
    {
        if ((int)Time.time % 4 == 0 || (int)Time.time % 4 == 1)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y + Time.deltaTime * bossSpeed, gameObject.transform.position.z);
        }
        else
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                gameObject.transform.position.y - Time.deltaTime * bossSpeed, gameObject.transform.position.z);
        }
    }

    public virtual int CheckPhase(float health, float maxHealth,float phaseRatio , int phase)
    {
        if ( Mathf.Approximately(health, (maxHealth * phaseRatio)))
        {
            print("true");
            return phase += 1;
        }

        return phase;
    }


    public virtual void CheckDestroy(float bossHealth)
    {
        if ( Mathf.Approximately(0f, bossHealth ))
        {
            Spawner.i.SpawnObject(Prefab.VictoryMenu, Vector3.zero);
            Destroy(gameObject);
        }
    }

    public virtual void modifyBossHealth(float value)
    {
        bossCurrentHealth += value;
        if (bossCurrentHealth >= bossMaxHealth)
        {
            bossCurrentHealth = bossMaxHealth;
        }


    }

}
