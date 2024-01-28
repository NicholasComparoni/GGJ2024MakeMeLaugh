using System;
using System.Collections;
using InputAndMovement;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class TeleportTarget : Target
{
    [SerializeField] private GameObject _arrivePoint;
    [SerializeField] private float _transitionTime;
    [SerializeField] private float _blackScreenTime;
    [SerializeField] private AudioClip _newSoundtrack;
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
        AudioSource soundtrack = Camera.main.gameObject.GetComponent<AudioSource>();
        BlackScreenCanvas.Instance.gameObject.SetActive(true);
        Image img = BlackScreenCanvas.Instance.gameObject.GetComponentInChildren<Image>();
        _player.StopVelocity();
        _player.enabled = false;
        _timer = 0;
        float startAlphaValue = img.color.a;
        float startVolumeValue = soundtrack.volume;
        while (_timer < _transitionTime)
        {
            _timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlphaValue, 1, _timer / _transitionTime);
            float newVolume = Mathf.Lerp(startVolumeValue, 0, _timer / _transitionTime);
            soundtrack.volume = newVolume;
            img.color = new Color(img.color.r, img.color.g, img.color.b, newAlpha);
            yield return null;
        }
        _timer = 0;
        Teleporting();
        soundtrack.clip = _newSoundtrack;
        soundtrack.Play();
        while (_timer < _blackScreenTime)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        _timer = 0;
        while (_timer < _transitionTime)
        {
            _timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1, startAlphaValue, _timer / _transitionTime);
            img.color = new Color(img.color.r, img.color.g, img.color.b, newAlpha);
            float newVolume = Mathf.Lerp(0, startVolumeValue, _timer / _transitionTime);
            soundtrack.volume = newVolume;
            yield return null;
        }
        _player.enabled = true;
    }
}