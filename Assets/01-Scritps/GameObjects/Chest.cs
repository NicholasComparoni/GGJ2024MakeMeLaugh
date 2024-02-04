using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Chest : Target
{
    [SerializeField] public List<Sprite> _keySprites;
    [SerializeField] public List<Sprite> _emptySprites;
    [SerializeField] public SpriteRenderer _mySprite;
    [SerializeField] public AudioClip _chestSound;
    [SerializeField] private bool _hasKey;
    public ExclamativePoint _xclPoint;
    private AudioSource _audioSource;
    private bool isChestOpen = false;
    public bool IsChestOpen
    {
        get { return isChestOpen;}
        private set { }
    }
    

    // private int _index = 0; // Assigned but never used

    public void Start()
    {
        _mySprite = GetComponentsInChildren<SpriteRenderer>()[0];
        _audioSource = GetComponent<AudioSource>();
    }
    public void OpenChest()
    {
        if (_hasKey)
        {
            _mySprite.sprite = _keySprites[0];

            PickupTarget target = gameObject.AddComponent<PickupTarget>();
            if (GetComponents<PickupTarget>().Length > 1)
            {
                Destroy(gameObject.GetComponents<PickupTarget>()[0]);
            }

            if (isChestOpen)
                target.PickUp();
            else
            {
                _audioSource.PlayOneShot(_chestSound);
            }
        }
        else
        {
            if (!isChestOpen)
            {
                _audioSource.PlayOneShot(_chestSound);
                _mySprite.sprite = _emptySprites[Random.Range(0,_emptySprites.Count)];
            }
        }
        isChestOpen=true;
    }
}
