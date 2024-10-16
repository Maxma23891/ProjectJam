using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // ตั้งค่าเริ่มต้นของ Slider ให้ตรงกับค่าที่เก็บไว้
        volumeSlider.value = PlayerPrefs.GetFloat("ClickVolume", 1f);

        // เพิ่ม Listener ให้ Slider
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
    }

    // ฟังก์ชันที่เรียกเมื่อ Slider เปลี่ยนค่า
    public void OnVolumeChange()
    {
        SoundManager soundManager = FindObjectOfType<SoundManager>();
        if (soundManager != null)
        {
            soundManager.SetVolume(volumeSlider.value); // ปรับระดับเสียงผ่าน SoundManager
        }
    }
}
