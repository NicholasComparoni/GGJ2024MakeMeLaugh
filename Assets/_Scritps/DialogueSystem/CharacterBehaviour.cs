using System;
using System.Collections;
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
        Speak();
    }

    //Functions
    public void Speak()
    {
        DialogueNameBox nameBox = DialogueCanvas.Instance.GetComponentInChildren<DialogueNameBox>();
        DialogueTextBox textBox = DialogueCanvas.Instance.GetComponentInChildren<DialogueTextBox>();
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
            nameBox.GetComponentInChildren<TMP_Text>().text = _charName;
            textBox.GetComponentInChildren<TMP_Text>().text = "Hai rotto il cazzo, non ripeto la stessa cosa due volte. LAVORAAAA!!!";
        }
    }
}