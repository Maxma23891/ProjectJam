using UnityEngine;
using UnityEngine.SceneManagement; // ����Ѻ�Ѵ��� Scene

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource; // ��ҧ�ԧ�֧ AudioSource ���ФǺ���
    private AudioClip currentClip; // ������Ѻ�Դ�����Ի�ŧ�����ѧ���

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // ��Ѥÿѧ��ѹ����� Scene �١��Ŵ
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // �����������§�����ѧ� Scene �á
        ChangeBackgroundMusic(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ����� Scene �١��Ŵ���� �������ŧ�����ѧ������� Scene
        ChangeBackgroundMusic(scene.name);
    }

    private void ChangeBackgroundMusic(string sceneName)
    {
        AudioClip newMusic = null;

        // ��˹��ŧ�����ѧ������� Scene
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
                newMusic = Resources.Load<AudioClip>("sound/lofi"); // �����§ lofi ����Ѻ Scene ����ҹ��
                break;
            // ����ö����������Ѻ Scene ��� � ��
            default:
                newMusic = null; // �ҡ��辺�ŧ����Ѻ Scene ��� ����駤���� null
                break;
        }

        // ��Ǩ�ͺ����ŧ����������ŧ�����ѧ�������
        if (newMusic != null)
        {
            // ����ŧ�����������͹�Ѻ�ŧ�����ѧ������� �������¹�ŧ
            if (currentClip != newMusic)
            {
                currentClip = newMusic; // �ѻവ�ŧ�����ѧ���
                PlayBackgroundMusic(); // ����ŧ����
            }
        }
        else
        {
            // ��� newMusic �� null �������¹���§�� "none"
            StopBackgroundMusic(); // ��ش���§
        }
    }

    private void PlayBackgroundMusic()
    {
        if (audioSource != null && currentClip != null)
        {
            audioSource.clip = currentClip; // ��˹���Ի�ŧ
            audioSource.loop = true; // ������ŧ��蹫��
            audioSource.volume = PlayerPrefs.GetFloat("volume", 1); // ��Ŵ�дѺ���§
            if (!audioSource.isPlaying) // ��Ǩ�ͺ������§������������
            {
                audioSource.Play(); // ���������ŧ
            }
        }
    }

    private void StopBackgroundMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); // ��ش���������§
            currentClip = null; // ���絤�Ի�ŧ�����ѧ���
        }
    }

    public void SetVolume(float value)
    {
        if (audioSource != null)
        {
            audioSource.volume = value; // ��駤�����§�ͧ AudioSource
            PlayerPrefs.SetFloat("volume", value); // �ѹ�֡����дѺ���§
            PlayerPrefs.Save(); // �ѹ�֡ŧ��ʡ�
        }
    }
}
