using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecoratedSliderText : MonoBehaviour
{
    public Slider slider; // ตัว Slider
    public TextMeshProUGUI handleText; // ข้อความตัวเลขบน Handle

    void Start()
    {
        // ตั้งค่า Slider ให้มีค่า 0 - 1
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 0.5f;

        // ปรับ Slider ให้ตอบสนองการเปลี่ยนแปลงค่า
        slider.onValueChanged.AddListener(UpdateHandleText);

        // อัพเดตตัวเลขใน Handle
        UpdateHandleText(slider.value);
    }

    void UpdateHandleText(float value)
    {
        // แสดงค่าของ Slider บน Handle พร้อมทศนิยม 3 ตำแหน่ง
        handleText.text = value.ToString("0.000");

        // ตกแต่งข้อความ
        DecorateText(handleText);
    }

    void DecorateText(TextMeshProUGUI text)
    {
        text.color = Color.white; // ตั้งค่าสีตัวอักษรเป็นสีขาว

        // สร้างกรอบสีดำรอบตัวอักษร
        text.fontSharedMaterial = new Material(text.fontSharedMaterial); // สร้าง Material ใหม่
        text.fontSharedMaterial.SetColor("_OutlineColor", Color.black); // ตั้งค่าสีกรอบเป็นสีดำ
        text.fontSharedMaterial.SetFloat("_OutlineWidth", 0.2f); // ตั้งค่าความหนาของกรอบ
    }
}
