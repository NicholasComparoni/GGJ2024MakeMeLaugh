using System;
using UnityEngine;

public class OptionsMenuCanvas : MonoBehaviour
{
    public static OptionsMenuCanvas Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
