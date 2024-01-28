using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Target
{
    [SerializeField] private Collider2D _mytrigger;
    [SerializeField] public List<Sprite> _sprites;
    [SerializeField] public SpriteRenderer _mySprite;
    [SerializeField] private AudioClip _chestSound;
    [SerializeField] private bool hasKey;
    private bool isChestOpen = false;
    

    private int _index = 0;

    public void Start()
    {
        _mySprite = GetComponentInChildren<SpriteRenderer>();

    }
    public void OpenChest()
    {
        if (hasKey)
        {
            _mySprite.sprite = _sprites[0];
            

            PickupTarget target = gameObject.AddComponent<PickupTarget>();

            if (isChestOpen)
                target.PickUp();

            isChestOpen=true;

        }
        else
        {
            _mySprite.sprite = _sprites[1];

        }


    }
}
