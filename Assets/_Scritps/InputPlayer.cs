using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace InputAndMovement
{
    public class InputPlayer : MonoBehaviour
    {
        [SerializeField] Character _target;
        Vector2 direction;


        void Start()
        {

        }


        void Update()
        {
            direction = Vector2.zero;
 

            if (Input.GetKey(KeyCode.D))
            {
               // Debug.Log("San Pellegrino");
                direction += Vector2.right;
                //_target._stepsNum++
            }
            if (Input.GetKey(KeyCode.A))
            {
               // Debug.Log("Lady Gabibba");
                direction += Vector2.left;
                //_target._stepsNum++


            }
            if (Input.GetKey(KeyCode.S))
            {

               // Debug.Log("Roventa");
                direction += Vector2.down;

                //_target._stepsNum++

            }
            if (Input.GetKey(KeyCode.W))     //{ direction += Vector2.up; }Conviene scrivere una singola instruzione di lato?
            {
               // Debug.Log("Rio Casamia");
                direction += Vector2.up;

                //_target._stepsNum++

            }
            _target.Move(direction);
            _target.RotateTransform(direction);
        }
    }

}
