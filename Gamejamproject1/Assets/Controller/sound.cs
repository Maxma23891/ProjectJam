using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;  // อ้างอิงถึง UI Slider
    public AudioSource audioSource;  // อ้างอิงถึง AudioSource ที่จะควบคุม

    void Start()
    {
        // ตรวจสอบว่ามีการบันทึกค่าระดับเสียงไว้ใน PlayerPrefs หรือไม่
        if (PlayerPrefs.HasKey("volume"))
        {
            // ถ้ามี ให้ตั้งค่า slider และระดับเสียงตามค่าที่บันทึกไว้
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
            audioSource.volume = volumeSlider.value;
        }
        else
        {
            // ถ้าไม่มี ให้ตั้งค่าเริ่มต้นที่ 1 (เสียงเต็ม)
            volumeSlider.value = 1;
            audioSource.volume = 1;
        }

        // กำหนด listener ให้ slider เรียกฟังก์ชัน SetVolume เมื่อมีการเปลี่ยนแปลง
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // ฟังก์ชันที่ใช้ควบคุมระดับเสียง
    public void SetVolume(float value)
    {
        audioSource.volume = value;  // ตั้งค่าเสียงของ AudioSource
        PlayerPrefs.SetFloat("volume", value);  // บันทึกค่าระดับเสียงลงใน PlayerPrefs
        PlayerPrefs.Save();  // บันทึกลงดิสก์
    }
}
