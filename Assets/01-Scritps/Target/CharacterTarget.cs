using InputAndMovement;
using UnityEngine;

[RequireComponent(typeof(CharacterBehaviour))]
public class CharacterTarget : Target
{
    public ExclamativePoint _xclPoint;
    private PlayerMovement _player;
    [SerializeField] private Transform finalPosition;
    private bool _isFinalPosition = false;

    private void Awake() {
        type = TargetType.CHARACTER;
    }

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }

    public void StartDialogue()
    {
        _player.StopVelocity();
        CharacterBehaviour._currentCharacterInteraction = gameObject.GetComponent<CharacterBehaviour>();
        CharacterBehaviour._currentCharacterInteraction.Speak(this);
        PlayerMovement.pInput.actions.FindActionMap("PlayerOnGround").FindAction("DialogueSkip").Enable();
        PlayerMovement.pInput.actions.FindActionMap("PlayerOnGround").FindAction("Move").Disable();
        PlayerMovement.pInput.actions.FindActionMap("DumbButtons").Disable();
    }

    public virtual void CloseDialogue()
    {
        DialogueCanvas.Instance.gameObject.SetActive(false);
        _player.enabled = true;
        // Move if a final position is provided
        if (!_isFinalPosition && finalPosition)
        {
            transform.position = finalPosition.position;
            _isFinalPosition = true;
        }
        PlayerMovement.pInput.actions.FindActionMap("PlayerOnGround").FindAction("DialogueSkip").Disable();
        PlayerMovement.pInput.actions.FindActionMap("PlayerOnGround").FindAction("Move").Enable();
        PlayerMovement.pInput.actions.FindActionMap("DumbButtons").Enable();
    }
}