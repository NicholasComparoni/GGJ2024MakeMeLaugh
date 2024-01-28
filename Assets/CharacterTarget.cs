using System;
using UnityEngine;

[RequireComponent(typeof(CharacterBehaviour))]
public class CharacterTarget : Target
{
    public ExclamativePoint _xclPoint;

    public void StartDialogue()
    {
        CharacterBehaviour._currentCharacterInteraction = gameObject.GetComponent<CharacterBehaviour>();
        CharacterBehaviour._currentCharacterInteraction.Speak(this);
    }

    public void CloseDialogue()
    {
        DialogueCanvas.Instance.gameObject.SetActive(false);
    }
}