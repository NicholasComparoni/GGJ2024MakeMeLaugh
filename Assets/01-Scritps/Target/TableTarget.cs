using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TableTarget : Target
{
    private static int _tableCounter = 0;
    private static const int _MAXCOUNTER = 10; // TODO = GetGameObjectsIntoScene.length

    public int isMaxCounter { get { return _tableCounter == _MAXCOUNTER; } private set { }}
    

    [SerializeField] private ExclamativePoint _xclPoint;

    [SerializeField] private SpriteRenderer _renderer; // the first sprite here should be set to dirty

    [SerializeField] private Sprite _cleanSprite;

    private bool _hasBeenCleaned = false;

    public bool HasBeenCleaned { get { return _hasBeenCleaned; } private set { } } 


    private void Awake() {
        type = TargetType.TABLE;
    }

    public void Clean()
    {
        if (!_hasBeenCleaned) {
            _xclPoint.gameObject.SetActive = false;
            _renderer.sprite = _cleanSprite;
            _tableCounter++;

            hasBeenCleaned = true;
        }
    }

    // TODO (optional): launch event to tell the teleport door to open
    // TODO (else): make the check in player like done with pickup objects
}
