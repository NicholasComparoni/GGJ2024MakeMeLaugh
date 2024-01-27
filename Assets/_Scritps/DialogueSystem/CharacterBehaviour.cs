using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] private string _charName;
    [SerializeField] private List<DialogueNode> _dialogue;
    [SerializeField] [TextArea] private string _lastDialogueText;
    public static CharacterBehaviour _currentCharacterInteraction;
    private int _dialogueIndex = 0;
    private bool _isFirtInteraction = true;

    [Serializable]
    struct DialogueNode
    {
        public float speakingTime;
        public Speaker currentSpeaker;
        [TextArea(3, 10)] public string dialogueText;
    }

    public enum Speaker
    {
        Character,
        Player
    }

    private void Start()
    {
        _currentCharacterInteraction = this;
    }

    //Functions
    public void Speak(CharacterTarget target)
    {
        DialogueCanvas.Instance.gameObject.SetActive(true);
        DialogueNameBox nameBox = DialogueCanvas.Instance.GetComponentInChildren<DialogueNameBox>();
        DialogueTextBox textBox = DialogueCanvas.Instance.GetComponentInChildren<DialogueTextBox>();
        if (_isFirtInteraction)
        {
            if (_dialogueIndex < _dialogue.Count)
            {
                if (_dialogue[_dialogueIndex].currentSpeaker == Speaker.Character)
                {
                    nameBox.GetComponentInChildren<TMP_Text>().text = _charName;
                }

                if (_dialogue[_dialogueIndex].currentSpeaker == Speaker.Player)
                {
                    PlayerBehaviour player = FindObjectOfType<PlayerBehaviour>();
                    nameBox.GetComponentInChildren<TMP_Text>().text = player.PlayerName;
                }

                textBox.GetComponentInChildren<TMP_Text>().text = _dialogue[_dialogueIndex].dialogueText;
                _dialogueIndex++;
            }
            else
            {
                target.CloseDialogue();
                _isFirtInteraction = false;
            }
        }
        else
        {
            if (_dialogueIndex > 0)
            {
                nameBox.GetComponentInChildren<TMP_Text>().text = _charName;
                textBox.GetComponentInChildren<TMP_Text>().text = _lastDialogueText;
                _dialogueIndex = 0;
            }
            else
            {
                target.CloseDialogue();
                _dialogueIndex++;
            }
        }
    }
}