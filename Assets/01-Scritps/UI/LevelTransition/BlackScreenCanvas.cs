using System;
using UnityEngine;

public class BlackScreenCanvas : MonoBehaviour
{
    public static BlackScreenCanvas Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"You can't have more than one object of type {gameObject.GetType()}");
        }

        Instance = this;
    }

    private void Start()
    {
        Instance.gameObject.SetActive(false);
    }
}