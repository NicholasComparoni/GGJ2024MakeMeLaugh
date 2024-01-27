using System;
using System.Collections;
using InputAndMovement;
using UnityEngine;
using UnityEngine.UI;

public class TeleportTarget : Target
{
    [SerializeField] private GameObject _arrivePoint;
    [SerializeField] private float _transitionTime;
    [SerializeField] private float _blackScreenTime;
    private PlayerMovement _player;
    private float _timer;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }

    public void Teleporting()
    {
        //Debug.Log("Mi Teletrasporto verso mille soli di dolore");
        _player.transform.position = _arrivePoint.transform.position;
    }

    public IEnumerator BlackScreenTransition()
    {
        BlackScreenCanvas.Instance.gameObject.SetActive(true);
        Image img = BlackScreenCanvas.Instance.gameObject.GetComponentInChildren<Image>();
        _player.StopVelocity();
        _player.enabled = false;
        _timer = 0;
        float startValue = img.color.a;
        while (_timer < _transitionTime)
        {
            _timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 1, _timer / _transitionTime);
            img.color = new Color(img.color.r, img.color.g, img.color.b, newAlpha);
            yield return null;
        }
        _timer = 0;
        Teleporting();
        while (_timer < _blackScreenTime)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        _timer = 0;
        while (_timer < _transitionTime)
        {
            _timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1, startValue, _timer / _transitionTime);
            img.color = new Color(img.color.r, img.color.g, img.color.b, newAlpha);
            yield return null;
        }
        _player.enabled = true;
    }
}