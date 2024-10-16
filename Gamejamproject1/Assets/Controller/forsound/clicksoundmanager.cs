using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance; // ใช้เก็บ instance เดียว
    private AudioSource audioSource;

    void Awake()
    {
        // ตรวจสอบว่า instance ถูกสร้างขึ้นหรือไม่
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
        audioSource.volume = PlayerPrefs.GetFloat("ClickVolume", 1f); // โหลดค่าระดับเสียงที่เก็บไว้ (ถ้ามี)
    }

    public void PlayClickSound()
    {
        audioSource.Play(); 
    }

    
    public void SetVolume(float volume)
    {
        audioSource.volume = volume; 
        PlayerPrefs.SetFloat("ClickVolume", volume); 
    }
}
