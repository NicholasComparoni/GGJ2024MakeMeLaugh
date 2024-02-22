using TMPro;
using UnityEngine;

public class DialoguePressA : MonoBehaviour
{
    public void SwitchTextColor(Color color)
    {
        GetComponentInChildren<TMP_Text>().color = color;
    }
}
