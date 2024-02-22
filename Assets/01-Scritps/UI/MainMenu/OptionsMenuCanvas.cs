using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuCanvas : MonoBehaviour
{
    public static OptionsMenuCanvas Instance;
    [SerializeField] private List<GameObject> elementsToTurnOffOnStart;

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
        foreach (GameObject obj in elementsToTurnOffOnStart)
        {
            obj.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
