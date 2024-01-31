using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PickupTarget : Target
{
    public static bool hasBeenReached = false;

    [SerializeField] public Collider2D _mytrigger;         //il trigger dentro la quale devi entrare


    public void PickUp()
    {
        //Debug.Log("Preso il pipo");
        hasBeenReached = true;
        //gameObject.SetActive(false);
        if (TryGetComponent<Chest>(out Chest chest))
        {
            chest._mySprite.sprite = chest._keySprites[1];
        }
    }
}

