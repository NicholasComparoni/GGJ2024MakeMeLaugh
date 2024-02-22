using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    private Slider _slider;
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = AudioManager.MusicVolume;
    }

    public void ChangeVolume()
    {
        AudioManager.MusicVolume = _slider.value;
        Debug.Log($"Music Volume: {_slider.value}");
        AudioManager.UpdateVolume();
    }
}
