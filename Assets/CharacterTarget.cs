using System;
using InputAndMovement;
using UnityEngine;
using UnityEngine.Windows.WebCam;

[RequireComponent(typeof(CharacterBehaviour))]
public class CharacterTarget : Target
{
    public ExclamativePoint _xclPoint;
    private PlayerMovement _player;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }

    public void StartDialogue()
    {
        _player.StopVelocity();
        _player.enabled = false;
        CharacterBehaviour._currentCharacterInteraction = gameObject.GetComponent<CharacterBehaviour>();
        CharacterBehaviour._currentCharacterInteraction.Speak(this);
    }

    public void CloseDialogue()
    {
        DialogueCanvas.Instance.gameObject.SetActive(false);
        _player.enabled = true;
    }
}