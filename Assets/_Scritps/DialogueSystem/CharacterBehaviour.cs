using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [SerializeField] private string _charName;
    [SerializeField] private List<DialogueNode> _dialogue;

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

    //Functions
    private void Start()
    {
        StartCoroutine(Speak());
    }

    public IEnumerator Speak()
    {
        DialogueNameBox nameBox = DialogueCanvas.Instance.GetComponentInChildren<DialogueNameBox>();
        DialogueTextBox textBox = DialogueCanvas.Instance.GetComponentInChildren<DialogueTextBox>();
        foreach (DialogueNode node in _dialogue)
        {
            if (node.currentSpeaker == Speaker.Character)
            {
                nameBox.GetComponentInChildren<TMP_Text>().text = _charName;
            }

            if (node.currentSpeaker == Speaker.Player)
            {
                PlayerBehaviour player = FindObjectOfType<PlayerBehaviour>();
                nameBox.GetComponentInChildren<TMP_Text>().text = player.PlayerName;
            }

            textBox.GetComponentInChildren<TMP_Text>().text = node.dialogueText;
            yield return new WaitForSeconds(node.speakingTime);
        }

        yield return 0;
    }
}