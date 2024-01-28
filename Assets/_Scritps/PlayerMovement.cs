using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace InputAndMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] public float _speed = 1;
        [SerializeField] public Rigidbody2D _rb;
        [SerializeField] SpriteRenderer _bodySprite;
        private PlayerBehaviour _playerBehaviour;
        private float angle = 0;
        private Vector2 moveInputValue;
        private Target _target; //A chi chiamare qualcosa
        private float _timer = 0;
        private AudioClip _stepSound;
        AudioSource _walkPitch;

        private void Start()
        {
            _playerBehaviour = FindObjectOfType<PlayerBehaviour>();
            _stepSound = _playerBehaviour.StepSound;
            _walkPitch = AudioManager.AudioSource;
        }

        public void Move(Vector2 direction)
        {
            MoveTransform(direction);
        }

        private void MoveTransform(Vector2 direction)
        {
            direction.Normalize();
            Debug.Log($"{direction.magnitude}");
            Vector2 deltaMove = direction * _speed * Time.deltaTime;
            transform.position = (Vector2)transform.position + deltaMove;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }

        public void RotateTransform(Vector2 direction)
        {
            if (moveInputValue.y > 0.1f)
            {
                angle = 0f;
            }
            else if (moveInputValue.y < -0.1f)
            {
                angle = 180f;
            }
            else if (moveInputValue.x > 0.1f)
            {
                angle = -90f;
            }
            else if (moveInputValue.x < -0.1f)
            {
                angle = 90f;
            }

            _bodySprite.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        public void OnMove(InputValue value)
        {
            if (_timer > .3f)
            {
                _walkPitch.pitch = _walkPitch.pitch - .1f + Random.Range(0f, .2f);
                if (_walkPitch.pitch > 1.1f || _walkPitch.pitch < 0.9f)
                {
                    _walkPitch.pitch = 1f;
                }
                _walkPitch.PlayOneShot(_stepSound);
                _timer = 0;
            }
            moveInputValue = value.Get<Vector2>();
            //Debug.Log(moveInputValue);
        }

        public void StopVelocity()
        {
            _rb.velocity = Vector2.zero;
        }

        private void MoveLogicController()
        {
            Vector2 result = moveInputValue * _speed;
            _rb.velocity = result;
        }

        private void FixedUpdate()
        {
            MoveLogicController();
            RotateTransform(moveInputValue);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.name);
            if (other.gameObject.TryGetComponent(out PickupTarget pickup))
                _target = pickup;

            if (other.gameObject.TryGetComponent(out TeleportTarget teleportus))
            {
                _target = teleportus;
                StartCoroutine(teleportus.BlackScreenTransition());
                _target = null;
            }

            if (other.gameObject.TryGetComponent(out CharacterTarget character))
                _target = character;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _target = null;
        }

        private void OnInteractionButton()
        {
            //Debug.Log("Patate al forno");
            if (_target?.GetType() == typeof(PickupTarget))
            {
                PickupTarget target = (PickupTarget)_target;
                target.PickUp();
                _target = null;
            }

            if (_target?.GetType() == typeof(CharacterTarget))
            {
                CharacterTarget target = (CharacterTarget)_target;
                target.StartDialogue();
                _target = null;
            }
        }

        private void OnDialogueSkip()
        {
            if (DialogueCanvas.Instance.gameObject.activeSelf)
            {
                CharacterBehaviour._currentCharacterInteraction.Speak(CharacterBehaviour._currentCharacterInteraction
                    .GetComponent<CharacterTarget>());
            }
        }
    }
}