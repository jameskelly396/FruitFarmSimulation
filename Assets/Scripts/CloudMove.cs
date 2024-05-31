using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    /* CloudMove.cs
     * By: James Kelly
     * Purpose: Simple script to move cloud from right to left and reset position after reaching a certain distance
     */
    public float moveSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3 (-moveSpeed * Time.deltaTime, 0,0);

        if(transform.position.x < -15)
        {
            transform.position += new Vector3( 30,0,0);
        }
    }
}
