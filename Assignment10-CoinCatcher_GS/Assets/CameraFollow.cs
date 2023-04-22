using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float camSpeed = 2f;
    public float yOffset = 1f;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffset, -10f);           //Get the player's position
        transform.position = Vector3.Slerp(transform.position, newPos, camSpeed*Time.deltaTime);        //Set target position to camera position
    }
}
