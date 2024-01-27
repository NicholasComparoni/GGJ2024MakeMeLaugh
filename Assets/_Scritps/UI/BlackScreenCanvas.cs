using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreenCanvas : MonoBehaviour
{
    [SerializeField] public  GameObject _panel;

    //public void Awake()
    //{
    //    if (_panel != null)
    //        //Debug.LogError("You can't have more than 2 instances BlackScreenCanvas in the same scene");
    //        _panel = this;
    //}
    public void Start()
    {
        _panel.gameObject.SetActive(false);
    }

}
