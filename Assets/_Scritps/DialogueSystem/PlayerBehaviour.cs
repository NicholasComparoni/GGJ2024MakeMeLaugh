using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private AudioClip _stepSound;
    [SerializeField] private string _playerName;

    public string PlayerName
    {
        get { return _playerName; }
    }

    public AudioClip StepSound
    {
        get { return _stepSound; }
    }
}