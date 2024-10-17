using UnityEngine;
using UnityEngine.SceneManagement; // สำหรับจัดการ Scene
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource; // อ้างอิงถึง AudioSource ที่จะควบคุม
    private AudioClip currentClip; // ใช้สำหรับติดตามคลิปเพลงที่กำลังเล่น
    private AudioSource secondaryAudioSource; // AudioSource ที่สองสำหรับการเล่นเพลงพร้อมกัน

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

    private void Start()
    {
        // สร้าง secondaryAudioSource ถ้ายังไม่มี
        if (secondaryAudioSource == null)
        {
            secondaryAudioSource = gameObject.AddComponent<AudioSource>();
        }

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
        if (sceneName == "C5-Main")
        {
            // โหลดเพลง menu และ Brownnoise 2
            AudioClip menuMusic = Resources.Load<AudioClip>("sound/menu");
            AudioClip brownNoiseMusic = Resources.Load<AudioClip>("sound/Brownnoise 2");

            // เริ่มต้นเล่นทั้งสองเพลงพร้อมกัน โดยทำการ fade
            StartCoroutine(PlayAndFadeBothMusic(menuMusic, brownNoiseMusic, 5f)); // fade 5 วินาที
        }
        else
        {
            // กรณีอื่นๆ ที่ไม่ใช่ C5-Main เล่นเพลงเดียวโดยไม่ทำ fade
            AudioClip newMusic = null;

            switch (sceneName)
            {
                case "Menu":
                    newMusic = Resources.Load<AudioClip>("sound/Menu");
                    break;
                case "option":
                    newMusic = Resources.Load<AudioClip>("sound/menu");
                    break;
                case "C2-1":
                    newMusic = Resources.Load<AudioClip>("sound/lofi");
                    break;
                case "askbf":
                case "askcake":
                case "asklib":
                    newMusic = Resources.Load<AudioClip>("sound/lofi");
                    break;
                case "C5-Main": // เผื่อกรณีผิดพลาด
                    newMusic = Resources.Load<AudioClip>("sound/Brownnoise 2");
                    break;
                    // เพิ่มเคสอื่นๆ ได้ที่นี่
            }

            // หากเป็นเพลงเดียวกันกับที่กำลังเล่นอยู่ ไม่ต้องทำอะไร
            if (newMusic != null && currentClip != newMusic)
            {
                currentClip = newMusic;
                PlayBackgroundMusic(); // เล่นเพลงเดียวแบบปกติ ไม่ทำ fade
            }
        }
    }

    private IEnumerator PlayAndFadeBothMusic(AudioClip menuClip, AudioClip brownNoiseClip, float duration)
    {
        // ตั้งค่า AudioSource หลักสำหรับเล่นเพลง menu
        audioSource.clip = menuClip;
        audioSource.loop = true;
        audioSource.volume = 1f; // เริ่มเสียง menu ที่ระดับเต็ม
        audioSource.Play();

        // ตั้งค่า secondaryAudioSource สำหรับเล่นเพลง Brownnoise 2
        secondaryAudioSource.clip = brownNoiseClip;
        secondaryAudioSource.loop = true;
        secondaryAudioSource.volume = 0f; // เริ่มเสียง brownnoise ที่ระดับ 0
        secondaryAudioSource.Play();

        float timer = 0f;

        // ทำการ fade ทั้งสองเสียงพร้อมกัน
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // ค่อย ๆ ลดเสียงของ menu ลง
            audioSource.volume = Mathf.Lerp(1f, 0f, progress);

            // ค่อย ๆ เพิ่มเสียงของ Brownnoise 2 ขึ้น
            secondaryAudioSource.volume = Mathf.Lerp(0f, 1f, progress);

            yield return null;
        }

        // เมื่อ fade เสร็จสิ้น, ทำให้ Brownnoise 2 ดังเต็มที่ และปิดเสียง menu
        audioSource.volume = 0f; // ปิดเสียง menu
        secondaryAudioSource.volume = 1f; // เสียง Brownnoise 2 ดังเต็มที่
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
