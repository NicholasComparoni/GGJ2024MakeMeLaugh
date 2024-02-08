using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] private string _charName;
    [SerializeField] private List<DialogueNode> _dialogue;
    [SerializeField] [TextArea] private string _lastDialogueText;
    [SerializeField] private AudioClip _lastDialogueClip;
    public static CharacterBehaviour _currentCharacterInteraction;
    private int _dialogueIndex = 0;
    private bool _isFirtInteraction = true;

    [Serializable]
    struct DialogueNode
    {
        [FormerlySerializedAs("soundToPLay")] public AudioClip soundToPlay;
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
    // Returns true if the dialogue can continue, else false
    public bool Speak(CharacterTarget target)
    {
        DialogueCanvas.Instance.gameObject.SetActive(true);
        DialogueNameBox nameBox = DialogueCanvas.Instance.GetComponentInChildren<DialogueNameBox>();
        DialogueTextBox textBox = DialogueCanvas.Instance.GetComponentInChildren<DialogueTextBox>();
        if (_isFirtInteraction)
        {
            if (_dialogueIndex < _dialogue.Count)
            {
                if (_dialogue[_dialogueIndex].soundToPlay != null)
                {
                    gameObject.GetComponent<AudioSource>().PlayOneShot(_dialogue[_dialogueIndex].soundToPlay);
                }
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
                return true;
            }
            else
            {
                target.CloseDialogue();
                _isFirtInteraction = false;
                return false;
            }
        }
        else
        {
            if (_dialogueIndex > 0 && _lastDialogueText != "")
            {
                nameBox.GetComponentInChildren<TMP_Text>().text = _charName;
                textBox.GetComponentInChildren<TMP_Text>().text = _lastDialogueText;
                gameObject.GetComponent<AudioSource>().PlayOneShot(_lastDialogueClip);
                _dialogueIndex = 0;
                return true;
            }
            else
            {
                target.CloseDialogue();
                _dialogueIndex++;
                return false;
            }
        }
    }
}