using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public static AudioSource AudioSource;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"There are more than one {gameObject.GetType()} object in this scene!");
        }
        Instance = this;
    }

    private void Start()
    {
        AudioSource = Instance.gameObject.GetComponent<AudioSource>();
    }
}
