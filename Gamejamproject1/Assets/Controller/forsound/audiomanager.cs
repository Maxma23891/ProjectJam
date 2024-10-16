using UnityEngine;
using UnityEngine.SceneManagement; // สำหรับจัดการ Scene

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource; // อ้างอิงถึง AudioSource ที่จะควบคุม
    private AudioClip currentClip; // ใช้สำหรับติดตามคลิปเพลงที่กำลังเล่น

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // สมัครฟังก์ชันเมื่อ Scene ถูกโหลด
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // เริ่มเล่นเสียงพื้นหลังใน Scene แรก
        ChangeBackgroundMusic(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // เมื่อ Scene ถูกโหลดใหม่ ให้เล่นเพลงพื้นหลังตามชื่อ Scene
        ChangeBackgroundMusic(scene.name);
    }

    private void ChangeBackgroundMusic(string sceneName)
    {
        AudioClip newMusic = null;

        // กำหนดเพลงพื้นหลังตามชื่อ Scene
        switch (sceneName)
        {
            case "Menu":
                newMusic = Resources.Load<AudioClip>("sound/Menu");
                break;
            case "option":
                newMusic = Resources.Load<AudioClip>("sound/menu");
                break;
            case "C2-1":
            case "C2-Alone-1":
            case "askbf":
            case "askcake":
            case "asklib":
            case "C2-Broadcast-1":
            case "extraterrestrial":
            case "C2-1S-1":
            case "C2-1S-2":
            case "C2-HellStop-1":
            case "C2-HellWorld-1":
            case "C2-1nameless":
            case "C2-Second-1":
            case "C2-Together-1":
                newMusic = Resources.Load<AudioClip>("sound/lofi"); // ใช้เสียง lofi สำหรับ Scene เหล่านี้
                break;
            // สามารถเพิ่มเคสสำหรับ Scene อื่น ๆ ได้
            default:
                newMusic = null; // หากไม่พบเพลงสำหรับ Scene นี้ ให้ตั้งค่าเป็น null
                break;
        }

        // ตรวจสอบว่าเพลงใหม่ไม่ใช่เพลงที่กำลังเล่นอยู่
        if (newMusic != null)
        {
            // ถ้าเพลงใหม่ไม่เหมือนกับเพลงที่กำลังเล่นอยู่ ให้เปลี่ยนเพลง
            if (currentClip != newMusic)
            {
                currentClip = newMusic; // อัปเดตเพลงที่กำลังเล่น
                PlayBackgroundMusic(); // เล่นเพลงใหม่
            }
        }
        else
        {
            // ถ้า newMusic เป็น null ให้เปลี่ยนเสียงเป็น "none"
            StopBackgroundMusic(); // หยุดเสียง
        }
    }

    private void PlayBackgroundMusic()
    {
        if (audioSource != null && currentClip != null)
        {
            audioSource.clip = currentClip; // กำหนดคลิปเพลง
            audioSource.loop = true; // ทำให้เพลงเล่นซ้ำ
            audioSource.volume = PlayerPrefs.GetFloat("volume", 1); // โหลดระดับเสียง
            if (!audioSource.isPlaying) // ตรวจสอบว่าเสียงไม่ได้เล่นอยู่
            {
                audioSource.Play(); // เริ่มเล่นเพลง
            }
        }
    }

    private void StopBackgroundMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // หยุดการเล่นเสียง
            currentClip = null; // รีเซ็ตคลิปเพลงที่กำลังเล่น
        }
    }

    public void SetVolume(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value; // ตั้งค่าเสียงของ AudioSource
            PlayerPrefs.SetFloat("volume", value); // บันทึกค่าระดับเสียง
            PlayerPrefs.Save(); // บันทึกลงดิสก์
        }
    }
}
