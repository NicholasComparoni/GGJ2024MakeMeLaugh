using UnityEngine;

public class InteractionCanvas : MonoBehaviour
{
    public static InteractionCanvas Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"You can have only one object of type {gameObject.GetType()} in your scene!");
        }
        Instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
