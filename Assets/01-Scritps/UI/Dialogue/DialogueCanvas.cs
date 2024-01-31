using UnityEngine;

public class DialogueCanvas : MonoBehaviour
{
    public static DialogueCanvas Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"You can have only one object of type {gameObject.GetType()} in your scene!");
        }
        Instance = this;
    }

    void Start()
    {
        gameObject.SetActive(false);
    }
}
