using InputAndMovement;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeleportTarget : Target
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _arrivePoint;
    [SerializeField] GameObject _panel;

    public void Teleporting()
    {
        //Debug.Log("Mi Teletrasporto verso mille soli di dolore");

        PlayerMovement _player = FindObjectOfType<PlayerMovement>();

        _player.enabled = false;
        _player.StopVelocity();
       _panel.gameObject.SetActive(true);

        _player.transform.position = _arrivePoint.transform.position;

        _panel.gameObject.SetActive(false);



    }

}
