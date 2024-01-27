using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace InputAndMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour
    {
        [SerializeField] float _speed = 1;
        [SerializeField] Rigidbody2D _rb;
        [SerializeField] SpriteRenderer _bodySprite;
        private float angle = 0;
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
            _bodySprite.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            if (direction == Vector2.up)
            {
                angle = 0f;
            }
            else if (direction == Vector2.down)
            {
                angle = 180f;
            }
            else if (direction == Vector2.left)
            {
                angle = 90f;
            }
            else if (direction == Vector2.right)
            {
                angle = -90f;
            }

        }

    }
    //    private void OnTriggerEnter(Collider other)
    //    {
    //        //Debug.Log("ONTRIGGERENTER");
    //        var interactable = other.GetComponentInParent<Interactable>();
    //        if (interactable is Interactable)
    //        {
    //            interactable.Interact();
    //            var coin = interactable.GetComponent<Coin>();    //Scrivibile anche come - interactable as Coin -
    //            if (coin != null)
    //            {
    //                _coinCollected += coin.GetValue();
    //                Debug.Log($"Interacted with {coin.name} coin collected {_coinCollected}");
    //            }
    //        }
    //    }
    //}
    //}
}

