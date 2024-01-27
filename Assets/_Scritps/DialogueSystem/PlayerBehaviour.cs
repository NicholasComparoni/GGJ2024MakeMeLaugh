using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private string _playerName;
    public string PlayerName
    {
        get { return _playerName; }
    }
}
