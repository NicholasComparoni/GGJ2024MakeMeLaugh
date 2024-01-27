using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PickupTarget : Target
{
    public bool hasBeenReached = false;
   
    [SerializeField] private Collider2D _mytrigger;         //il trigger dentro la quale devi entrare


    public void PickUp()
    {
        Debug.Log("Preso il pipo");
        hasBeenReached = true;
        gameObject.SetActive(false);
    }
}
