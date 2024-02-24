using System;
using TMPro;
using UnityEngine;

public class LanguageBox : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TMP_Text>().text = AudioManager.Language.ToString();
    }
}
