using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPhysics : MonoBehaviour
{
    /* FruitPhysics.cs
     * By: James Kelly
     * Purpose: To handle the physics of the fruit objects including growing, bouncing, breaking, and being handled by the player
     */
    [SerializeField] private float fallBreakSpeed = 10.0f;  
    [SerializeField] private float groundHeight = -3.5f;    
    [SerializeField] private float rightWallPosition = 9.3f; 
    [SerializeField] private float leftWallPosition = -9.3f;
    [SerializeField] private float growthSpeed = 0.005f;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private float horizontalThrownSpeed;
    private float fallHeight;
    //use these three values to alter bounce logic
    [SerializeField] private float bounceFallHeightDecay = 0.65f;
    [SerializeField] private float bouncVelocityYDecay = 0.45f;
    [SerializeField] private float bounceTimeClamp = 0.05f;

    //States
    public bool isAttachedToTree = true;
    public bool isHeldByPlayer = false;
    public bool isFalling = false;
    public bool hasMatured = false;
    public bool isBouncing = false;

    //Horizontal Movement variables from throwing fruit
    private Vector3 lastMousePosition;
    private Vector3 secondLastMousePosition;

    //planting seeds
    private bool hasCalculatedSeedChance = false;
    private bool containsSeed = false;
    [SerializeField] private GameObject seedPrefab;
    [SerializeField] private bool alwaysGenerateSeed = false; //Debug: remove random chance to generate seed
    [SerializeField] private float seedGenerationPercentage = 25;

    
    [SerializeField] private GameObject fruitParticleEffectPrefab;
    private Animator fruitAnim;
    private AudioSource audioAppleBounce;

    private void Awake()
    {
        transform.localScale = new Vector3(0.2f, 0.2f, 1f);
        hasMatured = false;
        fruitAnim = GetComponent<Animator>();
        audioAppleBounce = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Increase object's scale size until fruit is fully grown
        if (!hasMatured && isAttachedToTree)
        {
            transform.localScale += new Vector3(growthSpeed, growthSpeed, 0f);
            if (transform.localScale.x >= 1.0f && transform.localScale.y >= 1.0f)
            {
                hasMatured = true;
            }
        }

        //if fruit is mature and attached to tree, shake the fruit to get attention
        if(hasMatured && isAttachedToTree)
        {
            fruitAnim.SetTrigger("shake");

            //if fruit has fully matured before player picks it up, generate chance fruit contains a seed
            if (!hasCalculatedSeedChance)
            {
                hasCalculatedSeedChance = true;
                //Debug: set alwaysGenerateSeed to true to remove randomness
                if (alwaysGenerateSeed)
                    containsSeed = true;
                else
                {
                    //generate seed
                    if (Random.Range(1, 100) < seedGenerationPercentage)
                        containsSeed = true;
                    else
                        containsSeed = false;
                }
            }
        }

        //player has grabbed a fruit from a branch
        if (!isAttachedToTree && isHeldByPlayer)
        {
            //move fruit wherever player mouse is
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0.0f;
            transform.position = mousePosition;
            //track last location of mouse (used to calculate how hard player threw the fruit)
            secondLastMousePosition = lastMousePosition;
            lastMousePosition = Input.mousePosition;
            horizontalThrownSpeed = lastMousePosition.x - secondLastMousePosition.x;
        }

        //if fruit goes too far left or right offscreen, destroy the object
        if (transform.position.x + ((1 - transform.localScale.x) * 0.5) > (rightWallPosition) ||
            transform.position.x + ((1 - transform.localScale.x) * 0.5) < (leftWallPosition))
        {
            Destroy(gameObject);
        }

        //check if fruit has hit the ground, formula uses fruit's local scale so smaller fruits bounce at proper height
        if ((transform.position.y + ((1 - transform.localScale.y) * 0.5)) < (groundHeight) && !isHeldByPlayer)
        {
            SimulateLanding();
        }
        else if (!isAttachedToTree && !isHeldByPlayer)
        {
            StartFalling();

            velocity.x = horizontalThrownSpeed * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            isFalling = false;
        }

    }

    //Player has dropped the fruit, calculate distance to ground
    private void StartFalling()
    {
        if (!isFalling)
        {
            velocity.y = 0.0f;
            isFalling = true;
            fallHeight = CalculateDropHeight();
            Debug.Log("fall height:" + fallHeight);
        }
    }

    private void SimulateLanding()
    {
        //fruit has hit the ground too fast and has broken open
        if (Mathf.Abs(velocity.y) > fallBreakSpeed)
        {
            //if fruit contained a seed then create one where the fruit was broken
            if (containsSeed)
            {
                GameObject seed = Instantiate(seedPrefab, transform.position, Quaternion.identity);
                Vector3 seedPosition = new Vector3(seed.transform.position.x, -4, 0);
                seed.transform.position = seedPosition;
            }

            //create particle effect for destroyed fruit
            Quaternion rot = Quaternion.Euler(-90, 0, 0);
            Instantiate(fruitParticleEffectPrefab, transform.position, rot);

            //destroy fruit game object
            Destroy(gameObject);
        }
        else
        {
            //fruit bounce if not hitting the ground fast enough
            if (Mathf.Abs(velocity.y) > 0.1f)
            {
                fruitAnim.SetTrigger("bounce");
                audioAppleBounce.Play();
                //calculate bounce height based on distance from where fruit was initially dropped
                fallHeight = fallHeight * bounceFallHeightDecay;
                velocity.y = Mathf.Clamp(velocity.y, (velocity.y * bouncVelocityYDecay * -1) * fallHeight, 2); 
                transform.position += velocity * Mathf.Max(Time.deltaTime, bounceTimeClamp);
            }
            //fruit stops bouncing and rests on the ground
            else
            {
                velocity.y = 0.0f;
                horizontalThrownSpeed = 0.0f;
            }

        }
    }

    //shoots a raycast below the fruit when player drops it, returns distance to ground
    float CalculateDropHeight()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, -Vector2.up, 30, 1 << LayerMask.NameToLayer("Ground"));
        if (hitInfo.collider != null)
        {
            return hitInfo.distance;
        }

        return -1;
    }

}
