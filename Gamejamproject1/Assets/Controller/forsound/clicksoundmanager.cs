using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance; // ใช้สำหรับเก็บ instance เดียว
    private AudioSource audioSource;

    void Awake()
    {
        // ตรวจสอบว่ามี instance อยู่แล้วหรือไม่
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ทำให้ GameObject คงอยู่เมื่อเปลี่ยนฉาก
        }
        else
        {
            Destroy(gameObject); // ถ้ามี instance อยู่แล้ว ให้ทำลาย GameObject นี้
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("sound/click"); // โหลดเสียงคลิก
    }

    public void PlayClickSound()
    {
        audioSource.Play(); // เล่นเสียงคลิก
    }
}
