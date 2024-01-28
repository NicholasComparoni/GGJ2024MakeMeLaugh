using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoorCheck : MonoBehaviour
{
    [SerializeField] private GameObject _doorToOpen;
    public int allCleaned = 0;
public void OpeningDoor()
    {
        if (allCleaned == 5)
        {
            _doorToOpen.gameObject.SetActive(false);
        }
    }
}
