using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
        [SerializeField] private OptionsMenuCanvas _optionsMenuCanvas;
        public RuntimeAnimatorController _mcAnimationController;

        private PlayerBehaviour _playerBehaviour;

        // private float angle = 0; // Assigned but never used
        private Vector2 moveInputValue;
        private Target _target; //A chi chiamare qualcosa
        private float _timer = 0;
        private AudioClip _stepSound;
        public static PlayerInput pInput;
        public static AudioSource _walkPitch;

        private bool isDumbButtonPressed = false;
        private bool isDialogueOngoing = false;

        private enum DUMB_BUTTON
        {
            INVENTORY_BTN,
            MAP_BTN,
            DASH_BTN,
            ATTACK_BTN
        }


        private void Start()
        {
            _playerBehaviour = FindObjectOfType<PlayerBehaviour>();
            _optionsMenuCanvas.gameObject.SetActive(false);
            _stepSound = _playerBehaviour.StepSound;
            _walkPitch = AudioManager.AudioSource;
            pInput = GetComponent<PlayerInput>();
            pInput.actions.FindActionMap("PlayerOnGround").FindAction("DialogueSkip").Disable();
            pInput.actions.FindActionMap("DumbButtons").Enable();
        }

        public void Awake()
        {
            anim = GetComponent<Animator>();
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
            if (moveInputValue != Vector2.zero)
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
            }
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
                _target = teleportus;
                if (_target.type == TargetType.TELEPORT && PickupTarget.hasBeenReached)
                    // if(PickupTarget.hasBeenReached == true)
                {
                    StartCoroutine(teleportus.BlackScreenTransition());
                    PickupTarget.hasBeenReached = false;
                }
                else if (_target.type == TargetType.TELEPORT && TableTarget.isMaxCounter) // dirty
                {
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
            if (InteractionCanvas.Instance != null)
            {
                InteractionCanvas.Instance.gameObject.SetActive(false);
            }

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
            }

            if (_target?.GetType() == typeof(CharacterTarget))
            {
                CharacterTarget target = (CharacterTarget)_target;
                target.StartDialogue();
                isDialogueOngoing = true;
            }

            if (_target?.GetType() == typeof(Chest))
            {
                Chest target = (Chest)_target;
                target.OpenChest();
            }

            if (_target?.GetType() == typeof(TableTarget))
            {
                TableTarget target = (TableTarget)_target;
                target.Clean();
            }
            _target = null;
        }

        private void OnDialogueSkip()
        {
            if (DialogueCanvas.Instance.gameObject.activeSelf)
            {
                if (isDumbButtonPressed)
                {
                    DialogueCanvas.Instance.gameObject.SetActive(false);
                    isDumbButtonPressed = false;
                    pInput.actions.FindActionMap("PlayerOnGround").FindAction("DialogueSkip").Disable();
                    pInput.actions.FindActionMap("PlayerOnGround").FindAction("Move").Enable();
                    pInput.actions.FindActionMap("DumbButtons").Enable();
                }
                else
                {
                    isDialogueOngoing = CharacterBehaviour._currentCharacterInteraction.Speak(CharacterBehaviour
                        ._currentCharacterInteraction
                        .GetComponent<CharacterTarget>());
                }
            }
        }

        private void ShowDumbDialog(DUMB_BUTTON whichBtn)
        {
            DialogueCanvas.Instance.gameObject.SetActive(true);
            DialogueNameBox nameBox = DialogueCanvas.Instance.GetComponentInChildren<DialogueNameBox>();
            DialogueTextBox textBox = DialogueCanvas.Instance.GetComponentInChildren<DialogueTextBox>();
            
            pInput.actions.FindActionMap("PlayerOnGround").FindAction("DialogueSkip").Enable();
            pInput.actions.FindActionMap("PlayerOnGround").FindAction("Move").Disable();
            pInput.actions.FindActionMap("DumbButtons").Disable();
            
            nameBox.GetComponentInChildren<TMP_Text>().text = "Cinfa";
            switch (whichBtn)
            {
                case DUMB_BUTTON.INVENTORY_BTN:
                    textBox.GetComponentInChildren<TMP_Text>().text =
                        "Passa alla versione Premium a 59.99€ per sbloccare l'inventario!";
                    break;
                case DUMB_BUTTON.MAP_BTN:
                    textBox.GetComponentInChildren<TMP_Text>().text =
                        "Qui non prende, e Google Maps non funziona...";
                    break;
                case DUMB_BUTTON.DASH_BTN:
                    textBox.GetComponentInChildren<TMP_Text>().text =
                        "Ma lo vedi che panza? E vuoi pure scattare??";
                    break;
                case DUMB_BUTTON.ATTACK_BTN:
                    textBox.GetComponentInChildren<TMP_Text>().text =
                        "'Sta spada da cosplayer ti è costata una fortuna. NON usarla.";
                    break;
            }
        }

        private void OnInventoryDumbButton()
        {
            if (!isDialogueOngoing)
            {
                isDumbButtonPressed = true;
                ShowDumbDialog(DUMB_BUTTON.INVENTORY_BTN);
            }
        }

        private void OnMapDumbButton()
        {
            if (!isDialogueOngoing)
            {
                isDumbButtonPressed = true;
                ShowDumbDialog(DUMB_BUTTON.MAP_BTN);
            }
        }

        private void OnDashDumbButton()
        {
            if (!isDialogueOngoing)
            {
                isDumbButtonPressed = true;
                ShowDumbDialog(DUMB_BUTTON.DASH_BTN);
            }
        }

        private void OnAttackDumbButton()
        {
            if (!isDialogueOngoing)
            {
                isDumbButtonPressed = true;
                ShowDumbDialog(DUMB_BUTTON.ATTACK_BTN);
            }
        }

        private void OnPauseMenu()
        {
            if (SceneManager.GetActiveScene().name != "Menu")
            {
                pInput.actions.FindActionMap("PlayerOnGround").Disable();
                pInput.actions.FindActionMap("DumbButtons").Disable();
                _optionsMenuCanvas.gameObject.SetActive(true);
                GameObject.FindWithTag("OptionsMenu")?.GetComponentsInChildren<Button>()[0].Select();
            }
        }

        public void Resume()
        {
            pInput.actions.FindActionMap("PlayerOnGround").Enable();
            pInput.actions.FindActionMap("DumbButtons").Enable();
            _optionsMenuCanvas.gameObject.SetActive(false);
        }
    }
}