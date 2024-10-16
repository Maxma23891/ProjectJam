using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Singleton instance
    public AudioSource audioSource; // Reference to the AudioSource
    public AudioClip backgroundMusic; // Background music

    private void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent GameObject from being destroyed when switching scenes
        }
        else
        {
            Destroy(gameObject); // Destroy this GameObject if instance already exists
        }
    }

    void Start()
    {
        // Start playing background music
        if (audioSource != null && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.volume = PlayerPrefs.GetFloat("volume", 1); // Load saved volume
            audioSource.Play();
        }
    }

    // Method to set the volume from the Options slider
    public void SetVolume(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value; // Update audio source volume
            PlayerPrefs.SetFloat("volume", value); // Save volume setting
            PlayerPrefs.Save();
        }
    }
}
