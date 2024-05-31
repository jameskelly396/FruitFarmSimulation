using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    /* DestroyOverTime.cs
    * By: James Kelly
    * Purpose: Used to destroy a game object after a certain length of time has passed
    */
    private float timer = 0.0f;
    public float destroyAfterXseconds;
    
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > destroyAfterXseconds)
            Destroy(gameObject);
    }
}
