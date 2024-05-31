using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour
{
    /* TreeGrowth.cs
     * By: James Kelly
     * Purpose: Grows a tree that had been created from watering a seed. Plays Audio. Script is used in GrowFruits.cs
     */
    public float growthSpeed = 0.01f;
    public bool hasMatured = false;
    [SerializeField] AudioSource audioTreeGrow;
    private void Awake()
    {
        audioTreeGrow.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (!hasMatured)
        {
            transform.localScale += new Vector3(growthSpeed, growthSpeed, 0f);
            transform.position += new Vector3(0, growthSpeed*5, 0);
            if (transform.localScale.x >= 1.0f && transform.localScale.y >= 1.0f)
            {
                hasMatured = true;
                audioTreeGrow.Pause();
            }
        }
    }

}
