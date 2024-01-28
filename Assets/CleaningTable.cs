using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningTable : Target
{
    [SerializeField] private Collider2D _mytriggerus;
    [SerializeField] public OpenDoorCheck _myController;
    [SerializeField] private SpriteRenderer _mysprite;
    [SerializeField] private SpriteRenderer _mysprite2;
    private bool _hasBeenCleaned = false;
    [SerializeField]private int _cleanedTimes = 0;
    public void Cleaning()
    {
        _mysprite.GetComponent<SpriteRenderer>();

        _cleanedTimes++;

        if (_cleanedTimes == 5)
        {
            _hasBeenCleaned = true;

            _myController.allCleaned++;
            
            _mysprite.sprite = _mysprite2.sprite;
        }
    }
}