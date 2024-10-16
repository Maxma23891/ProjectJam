using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance; // ������Ѻ�� instance ����
    private AudioSource audioSource;

    void Awake()
    {
        // ��Ǩ�ͺ����� instance ���������������
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
    }

    public void PlayClickSound()
    {
        audioSource.Play(); // ������§��ԡ
    }
}
