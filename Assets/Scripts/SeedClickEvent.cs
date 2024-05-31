using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SeedClickEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /* SeedClickEvent.cs
     * By: James Kelly
     * Purpose: logic to allow seed to be moved around by player.
     */
    [SerializeField] private bool isHeldByPlayer = false;
    [SerializeField] private float groundHeight = -4f;
    [SerializeField] private Vector3 velocity;
    public bool isFalling = false;
    private float gravity = -9.81f;

    public void OnPointerDown(PointerEventData eventData)
    {
        isHeldByPlayer = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeldByPlayer = false;
    }

    private void FixedUpdate()
    {
        if (isHeldByPlayer)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0.0f;
            transform.position = mousePosition;
        }

        //Landed
        if ((transform.position.y + ((1 - transform.localScale.y) * 0.5)) < (groundHeight))
        {
            if (!isHeldByPlayer)
                velocity.y = 0.0f;
        }
        else if (!isHeldByPlayer)
        {
            StartFalling();

            velocity.y += gravity * Time.deltaTime;
            transform.position += velocity * Time.deltaTime;
        }
        else
        {
            isFalling = false;
        }
    }

    private void StartFalling()
    {
        if (!isFalling)
        {
            velocity.y = 0.0f;
            isFalling = true;
        }
    }
}
