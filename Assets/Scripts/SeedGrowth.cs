using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrowth : MonoBehaviour
{
    /* SeedGrowth.cs
     * By: James Kelly
     * Purpose: Checks if cloud is above seed and raining. Will generate tree prefab if true
     */
    private GameObject cloud;
    private CloudClickEvent cloudClickEvent;
    public GameObject treePrefab;

    private void Awake()
    {
        cloud = GameObject.Find("Cloud");
        cloudClickEvent = cloud.GetComponent<CloudClickEvent>();
    }

    // Update is called once per frame
    void Update()
    {
         //Cloud is raining above seed
        if(cloudClickEvent.isRaining && IsCloudAboveSeed())
        {
            GameObject tree = Instantiate(treePrefab, transform.position, Quaternion.identity);
            tree.transform.position = new Vector3(tree.transform.position.x, -4, 0);
            tree.GetComponent<TreeGrowth>().hasMatured = false;
            tree.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
            Destroy(gameObject);
        }
    }

    bool IsCloudAboveSeed()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(cloud.transform.position, -Vector2.up, 10, 1<< LayerMask.NameToLayer("Seed"));
        if (hitInfo.collider != null)
        {
            return true;
        }

        return false;
    }
}
