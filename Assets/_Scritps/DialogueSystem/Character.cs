using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Character : MonoBehaviour
{
    [SerializeField] private string _charName;
    [SerializeField]private List<DialogueNode> _dialogue;

    [Serializable]
    struct DialogueNode
    {
        public float speakingTime;
        public Speaker currentSpeaker;
        [TextArea(3,10)]public string dialogueText;
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
        foreach (DialogueNode node in _dialogue)
        {
            if (node.currentSpeaker == Speaker.Character)
            {
                Debug.Log(_charName);   
            }
            if (node.currentSpeaker == Speaker.Player)
            {
                PlayerBehaviour player = FindObjectOfType<PlayerBehaviour>();
                Debug.Log(player.PlayerName);
            }
            Debug.Log(node.dialogueText);
            yield return new WaitForSeconds(node.speakingTime);
        }
        yield return 0;
    }
}
