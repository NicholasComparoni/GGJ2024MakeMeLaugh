using System.Collections;
using InputAndMovement;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TeleportTarget : Target
{
    [SerializeField] private GameObject _arrivePoint;
    [SerializeField] private float _transitionTime;
    [SerializeField] private float _blackScreenTime;
    [SerializeField] private AudioClip _newSoundtrack;
    [SerializeField] private Sprite _newTextBoxSprite;
    [SerializeField] private Sprite _newNameBoxSprite;
    [SerializeField] private Color _newTextColor;
    [SerializeField] private Color _newPressYColor;
    private PlayerMovement _player;
    private float _timer;

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }

    public void Teleporting()
    {
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
        _player.anim.runtimeAnimatorController = _player._mcAnimationController;
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
        BlackScreenCanvas.Instance.gameObject.SetActive(false);
        _player.enabled = true;
        GameObject dialogueCanvas = DialogueCanvas.Instance.gameObject;
        dialogueCanvas.GetComponentInChildren<DialogueTextBox>().gameObject.GetComponent<Image>().sprite = _newTextBoxSprite;
        dialogueCanvas.GetComponentInChildren<DialogueTextBox>().gameObject.GetComponentInChildren<TMP_Text>().color = _newTextColor;
        dialogueCanvas.GetComponentInChildren<DialogueNameBox>().gameObject.GetComponent<Image>().sprite = _newNameBoxSprite;
        dialogueCanvas.GetComponentInChildren<DialogueNameBox>().gameObject.GetComponentInChildren<TMP_Text>().color = _newTextColor;
        dialogueCanvas.GetComponentInChildren<DialoguePressY>().SwitchTextColor(_newPressYColor);
    }
}