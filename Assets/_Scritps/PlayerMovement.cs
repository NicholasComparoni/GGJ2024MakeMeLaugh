using UnityEngine;
using UnityEngine.InputSystem;

namespace InputAndMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] float _speed = 1;
        [SerializeField] Rigidbody2D _rb;
        [SerializeField] SpriteRenderer _bodySprite;
        private float angle = 0;
        private Vector2 moveInputValue;
        private Target _target; //A chi chiamare qualcosa

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
            moveInputValue = value.Get<Vector2>();
            Debug.Log(moveInputValue);
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
            //Debug.Log(other.name);
            if (other.TryGetComponent<PickupTarget>(out PickupTarget pickup))
                _target = pickup;


            if (other.TryGetComponent<TeleportTarget>(out TeleportTarget teleportus))
            {
                _target = teleportus;
                teleportus.Teleporting();
                _target = null;
            }
        }

        private void OnInteractionButton()
        {
            //Debug.Log("Patate al forno");

            if (_target.GetType() == typeof(PickupTarget))
            {
                PickupTarget target = (PickupTarget)_target;
                target.PickUp();
                _target = null;
            }
        }


        private void OnDialogueSkip()
        {
            if (DialogueCanvas.Instance.gameObject.activeSelf)
            {
                CharacterBehaviour._currentCharacterInteraction.Speak();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _target = null;
        }
    }
}