using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        // ��駤��������鹢ͧ Slider ���ç�Ѻ��ҷ�������
        volumeSlider.value = PlayerPrefs.GetFloat("ClickVolume", 1f);

        // ���� Listener ��� Slider
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
    }

    // �ѧ��ѹ������¡����� Slider ����¹���
    public void OnVolumeChange()
    {
        SoundManager soundManager = FindObjectOfType<SoundManager>();
        if (soundManager != null)
        {
            soundManager.SetVolume(volumeSlider.value); // ��Ѻ�дѺ���§��ҹ SoundManager
        }
    }
}
