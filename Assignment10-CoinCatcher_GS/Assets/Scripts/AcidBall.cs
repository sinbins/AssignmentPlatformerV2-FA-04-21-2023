using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AcidBall : MonoBehaviour
{
    public GameObject acidBall;
    public Vector3 originalPos;
    public Collision2D collision;
    //public GameObject respawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        originalPos = acidBall.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

         

            if (collision.gameObject.tag == "Killzone")                  //If the collision tag is "Killzone", the player loses a life and respawns to respawn point
            {
                StartCoroutine("ResetAcid");

            }

        


    }
 

    IEnumerator ResetAcid()
    {
        yield return new WaitForSecondsRealtime(4);
        acidBall.transform.position = originalPos;
    }
}
