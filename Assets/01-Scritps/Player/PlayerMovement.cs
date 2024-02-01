using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace InputAndMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] public Animator anim;
        [SerializeField] public float _speed = 1;
        [SerializeField] public Rigidbody2D _rb;
        [SerializeField] private SpriteRenderer _bodySprite;
        public RuntimeAnimatorController _mcAnimationController;
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

        public void Awake()
        {
            anim = GetComponent<Animator>();
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
            anim.SetFloat("GoVertical", moveInputValue.y);
            anim.SetFloat("GoHorizontal", moveInputValue.x);


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
            InteractionCanvas.Instance.gameObject.SetActive(true);
            Debug.Log(other.name);
            if (other.gameObject.TryGetComponent(out PickupTarget pickup))
                _target = pickup;

            if (other.gameObject.TryGetComponent(out TeleportTarget teleportus))
            {
                // TODO: we could check teleport with target to distinguish between levels
                if (_target.type == TargetType.PICKUP && PickupTarget.hasBeenReached) 
                // if(PickupTarget.hasBeenReached == true)
                {
                    _target = teleportus;
                    StartCoroutine(teleportus.BlackScreenTransition());
                }
                else if (_target.type == TargetType.TABLE && ((TableTarget)_target).isMaxCounter) // dirty
                {
                    _target = teleportus;
                    StartCoroutine(teleportus.BlackScreenTransition());
                }
                _target = null;

                
            }

            if (other.gameObject.TryGetComponent(out CharacterTarget character))
            {
                _target = character;
                character._xclPoint.gameObject.SetActive(true);
            }

            if (other.gameObject.TryGetComponent(out Chest chest))
            {
                _target = chest;
                if (chest.TryGetComponent(out PickupTarget pTarget))
                {
                    if (!PickupTarget.hasBeenReached)
                    {
                        chest._xclPoint.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (!chest.IsChestOpen)
                    {
                        chest._xclPoint.gameObject.SetActive(true);
                    }
                }
                
            }

            if (other.gameObject.TryGetComponent(out TableTarget table))
            {
                if (!table.HasBeenCleaned) 
                    table._xclPoint.gameObject.SetActive(true);
                _target = table;   
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            InteractionCanvas.Instance.gameObject?.SetActive(false);
            if (other.gameObject.TryGetComponent(out CharacterTarget character))
            {
                character._xclPoint.gameObject.SetActive(false);
            }
            if (other.gameObject.TryGetComponent(out Chest chest))
            {
                chest._xclPoint.gameObject.SetActive(false);
            }
            if (other.gameObject.TryGetComponent(out TableTarget table))
            {
                table._xclPoint.gameObject.SetActive(false);
            }
            _target = null;
        }

        private void OnInteractionButton()
        {
            InteractionCanvas.Instance.gameObject.SetActive(false);
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
            if (_target?.GetType() == typeof(Chest))
            {
                Chest target = (Chest)_target;
                target.OpenChest();
                _target = null; 
            }
            if (_target?.GetType() == typeof(TableTarget))
            {
                TableTarget target = (TableTarget)_target;
                target.Clean();
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