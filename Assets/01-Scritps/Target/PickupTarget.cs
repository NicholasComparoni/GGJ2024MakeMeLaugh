using UnityEngine;

public class PickupTarget : Target
{
    public static bool hasBeenReached = false;

    [SerializeField] public Collider2D _mytrigger;         //il trigger dentro la quale devi entrare


    public void PickUp()
    {
        //Debug.Log("Preso il pipo");
        hasBeenReached = true;
        //gameObject.SetActive(false);
        if (TryGetComponent(out Chest chest))
        {
            chest._mySprite.sprite = chest._keySprites[1];
        }
    }
}

