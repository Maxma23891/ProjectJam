using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioClip backgroundMusic;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (audioSource != null && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.volume = PlayerPrefs.GetFloat("volume", 1);
            audioSource.Play();
        }
    }

    public void SetVolume(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value;
            PlayerPrefs.SetFloat("volume", value);
            PlayerPrefs.Save();
        }
    }
}
