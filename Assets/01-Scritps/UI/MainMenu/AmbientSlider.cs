using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AmbientSlider : MonoBehaviour
{
    private Slider _slider;
    private IEnumerator Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = AudioManager.AmbientVolume;
        yield return null;
    }

    public void ChangeVolume()
    {
        AudioManager.AmbientVolume = _slider.value;
        Debug.Log("Ambient Volume: " + _slider.value);
        AudioManager.UpdateVolume();
    }
}
