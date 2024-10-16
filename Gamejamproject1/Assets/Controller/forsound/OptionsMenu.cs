
using UnityEngine;
using UnityEngine.UI;

public class AudioOptionsMenu : MonoBehaviour 
{
    public Slider volumeSlider;

    void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("volume", 1);
            volumeSlider.onValueChanged.AddListener(delegate { OnVolumeSliderChanged(); });
        }
    }

    void OnVolumeSliderChanged()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.SetVolume(volumeSlider.value);
        }
    }
}
