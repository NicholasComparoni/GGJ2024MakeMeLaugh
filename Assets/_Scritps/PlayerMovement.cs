using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            moveInputValue =value.Get<Vector2>();
            Debug.Log(moveInputValue);
        }        
        private void OnInteractionButton()
        {
            //Debug.Log("Patate al forno");
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
            OnInteractionButton();

        }
    }
}

