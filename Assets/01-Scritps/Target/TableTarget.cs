using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTarget : Target
{
    private static int _tableCounter = 0;
    private const int _MAXCOUNTER = 15; // TODO get the number of tables (TableTarget objects) at run time.
                                        // Fast solution is Object.FindObjectOfType<T>() but quite inefficient.
                                        // Best would be to register each TableTarget to a GameManager in its Awake(),
                                        // this implies _MAXCOUNTER is Game Manager stuff (technically all of the level
                                        // logic shold be in it, not in the player)

    public static bool isMaxCounter { get { return _tableCounter == _MAXCOUNTER; } private set { }}
    

    public ExclamativePoint _xclPoint;

    [SerializeField] private SpriteRenderer _renderer; // the first sprite here should be set to dirty

    [SerializeField] private Sprite _cleanSprite;

    private bool _hasBeenCleaned = false;

    public bool HasBeenCleaned { get { return _hasBeenCleaned; } private set { } } 


    private void Awake() {
        type = TargetType.TABLE;
        // TODO subscribe to game manager to get
    }

    public void Clean()
    {
        if (!_hasBeenCleaned) {
            _xclPoint.gameObject.SetActive(false);
            _renderer.sprite = _cleanSprite;
            _tableCounter++;

            _hasBeenCleaned = true;
        }
    }

    public static void ResetTableCounter()
    {
        _tableCounter = 0;
    }
    
    // TODO (optional): launch event to tell the teleport door to open
    // TODO (else): make the check in player like done with pickup objects
}
