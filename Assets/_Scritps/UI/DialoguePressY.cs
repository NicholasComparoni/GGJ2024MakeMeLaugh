using TMPro;
using UnityEngine;

public class DialoguePressY : MonoBehaviour
{
    public void SwitchTextColor(Color color)
    {
        GetComponentInChildren<TMP_Text>().color = color;
    }
}
