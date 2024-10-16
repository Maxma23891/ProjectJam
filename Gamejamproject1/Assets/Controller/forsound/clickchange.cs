using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("ClickVolume", 1f);
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
    }

    public void OnVolumeChange()
    {
        SoundManager soundManager = FindObjectOfType<SoundManager>();
        if (soundManager != null)
        {
            soundManager.SetVolume(volumeSlider.value);
        }
    }
}
