using UnityEngine;

public class MouseClickSound : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // เช็คว่ามีการคลิกซ้าย
        {
            // ค้นหา SoundManager ใน Scene และเรียกใช้ PlayClickSound
            SoundManager soundManager = FindObjectOfType<SoundManager>();
            if (soundManager != null)
            {
                soundManager.PlayClickSound();
            }
        }
    }
}
