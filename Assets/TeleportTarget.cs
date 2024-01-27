using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTarget : Target
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _arrivePoint;

    // Update is called once per frame
    public void Teleporting()
    {
        Debug.Log("Mi Teletrasporto verso mille soli di dolore");
        //Vector2 playerpos = _player.GetComponent<Vector2>();

       _player.transform.position = _arrivePoint.transform.position;

    }
}
