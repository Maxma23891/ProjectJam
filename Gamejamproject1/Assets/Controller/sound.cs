using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;  // ��ҧ�ԧ�֧ UI Slider
    public AudioSource audioSource;  // ��ҧ�ԧ�֧ AudioSource ���ФǺ���

    void Start()
    {
        // ��Ǩ�ͺ����ա�úѹ�֡����дѺ���§���� PlayerPrefs �������
        if (PlayerPrefs.HasKey("volume"))
        {
            // ����� ����駤�� slider ����дѺ���§�����ҷ��ѹ�֡���
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
            audioSource.volume = volumeSlider.value;
        }
        else
        {
            // �������� ����駤��������鹷�� 1 (���§���)
            volumeSlider.value = 1;
            audioSource.volume = 1;
        }

        // ��˹� listener ��� slider ���¡�ѧ��ѹ SetVolume ������ա������¹�ŧ
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // �ѧ��ѹ�����Ǻ����дѺ���§
    public void SetVolume(float value)
    {
        audioSource.volume = value;  // ��駤�����§�ͧ AudioSource
        PlayerPrefs.SetFloat("volume", value);  // �ѹ�֡����дѺ���§ŧ� PlayerPrefs
        PlayerPrefs.Save();  // �ѹ�֡ŧ��ʡ�
    }
}
