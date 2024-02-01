using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// aka TableTarget TODO: THIS FILE SHOULD NOT BE USED ANYMORE
public class CleaningTable : Target 
{
    public ExclamativePoint _xclPoint;
    [SerializeField] private Collider2D _mytriggerus;
    [SerializeField] public OpenDoorCheck _myController;

    [SerializeField] private SpriteRenderer _mysprite; // the first dirty

    [SerializeField] private Sprite _myspriteDirty1;
    [SerializeField] private Sprite _myspriteDirty2;
    [SerializeField] private Sprite _myspriteClean;
    private bool _hasBeenCleaned = false;

    public bool HasBeenCleaned { get { return _hasBeenCleaned; } private set { } }

    private bool isA;
    [SerializeField] private int _cleanedTimes = 0;

    public bool Cleaning()
    {
        if (!_hasBeenCleaned) {
            _mysprite.sprite = _myspriteClean;
            _hasBeenCleaned = true;
            return true;
        }
        else return false;

        _mysprite.sprite = isA ? _myspriteDirty1 : _myspriteDirty2;
        isA = !isA;
        _cleanedTimes++;

        if (_cleanedTimes == 5)
        {
            _hasBeenCleaned = true;
            _myController.OpenDoor();
            _mysprite.sprite = _myspriteClean;
        }
    }
}