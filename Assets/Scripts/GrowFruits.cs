using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowFruits : MonoBehaviour
{
    /* GrowFruits.cs
     * By: James Kelly
     * Purpose: To grow fruits on a tree's branch. Animates and plays sounds when fruit is plucked.
     */
    public Transform spawnPoint;
    public GameObject fruitPrefab;
    private FruitPhysics fruitPhysics;
    private GameObject fruitObject;
    public Animator branchAnim;
    public GameObject tree;
    private TreeGrowth treeGrowth;
    private AudioSource audioTreeGrow;

    private bool hasBranchShook = false;
    // Start is called before the first frame update
    void Start()
    {
        treeGrowth = tree.GetComponent<TreeGrowth>();
        audioTreeGrow = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (treeGrowth.hasMatured)
        {
            //the last fruit from this branch was destroyed, grow a new fruit
            if (fruitObject == null)
            {
                fruitObject = Instantiate(fruitPrefab, spawnPoint.position, spawnPoint.rotation);
                fruitPhysics = fruitObject.GetComponent<FruitPhysics>();
                hasBranchShook = false;
            }
            else
            {
                //player has plucked fruit off branch
                if (fruitPhysics.isHeldByPlayer && !hasBranchShook)
                {
                    branchAnim.SetTrigger("shake");
                    hasBranchShook = true; 
                    audioTreeGrow.Play();
                }
            }
        }
    }
}
