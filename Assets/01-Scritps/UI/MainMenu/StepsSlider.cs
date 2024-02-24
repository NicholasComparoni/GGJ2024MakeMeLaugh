using UnityEngine;
using UnityEngine.UI;

public class StepsSlider : MonoBehaviour
{
    private Slider _slider;
    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = AudioManager.StepsVolume;
    }

    public void ChangeVolume()
    {
        AudioManager.StepsVolume = _slider.value;
        Debug.Log($"Steps Volume: {_slider.value}");
        AudioManager.UpdateVolume();
    }
}