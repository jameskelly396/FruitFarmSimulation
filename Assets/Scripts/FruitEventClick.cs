using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FruitEventClick : MonoBehaviour, IPointerMoveHandler, IPointerDownHandler, IPointerUpHandler
{
    /* FruitEventClick.cs
     * By: James Kelly
     * Purpose: To update fruit physics when player has clicked on the fruit. Logic is used in FruitsPhysics.cs
     */
    FruitPhysics fruitPhysics;
    private void Awake()
    {
        fruitPhysics = GetComponent<FruitPhysics>();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Debug.Log(name + " OnPointerMove");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(name + " OnPointerDown");
        if (fruitPhysics.isAttachedToTree)
        {
            fruitPhysics.isAttachedToTree = false;
        }
        if (!fruitPhysics.isHeldByPlayer)
        {
            fruitPhysics.isHeldByPlayer = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(name + " OnPointerUp");
        if (fruitPhysics.isHeldByPlayer)
        {
            fruitPhysics.isHeldByPlayer = false;
        }
    }

}
