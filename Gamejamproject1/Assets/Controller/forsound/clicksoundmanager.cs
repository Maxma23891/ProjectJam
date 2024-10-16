using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance; // ���� instance ����
    private AudioSource audioSource;

    void Awake()
    {
        // ��Ǩ�ͺ��� instance �١���ҧ����������
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ����� GameObject ���������������¹�ҡ
        }
        else
        {
            Destroy(gameObject); // ����� instance �������� ������� GameObject ���
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("sound/click"); // ��Ŵ���§��ԡ
        audioSource.volume = PlayerPrefs.GetFloat("ClickVolume", 1f); // ��Ŵ����дѺ���§�������� (�����)
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
