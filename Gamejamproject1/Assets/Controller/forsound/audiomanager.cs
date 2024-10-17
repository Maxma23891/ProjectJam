using UnityEngine;
using UnityEngine.SceneManagement; // ����Ѻ�Ѵ��� Scene
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource; // ��ҧ�ԧ�֧ AudioSource ���ФǺ���
    private AudioClip currentClip; // ������Ѻ�Դ�����Ի�ŧ�����ѧ���
    private AudioSource secondaryAudioSource; // AudioSource ����ͧ����Ѻ�������ŧ������ѹ

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

    private void Start()
    {
        // ���ҧ secondaryAudioSource ����ѧ�����
        if (secondaryAudioSource == null)
        {
            secondaryAudioSource = gameObject.AddComponent<AudioSource>();
        }

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
        if (sceneName == "C5-Main")
        {
            // ��Ŵ�ŧ menu ��� Brownnoise 2
            AudioClip menuMusic = Resources.Load<AudioClip>("sound/menu");
            AudioClip brownNoiseMusic = Resources.Load<AudioClip>("sound/Brownnoise 2");

            // ���������蹷���ͧ�ŧ������ѹ �·ӡ�� fade
            StartCoroutine(PlayAndFadeBothMusic(menuMusic, brownNoiseMusic, 5f)); // fade 5 �Թҷ�
        }
        else
        {
            // �ó����� �������� C5-Main ����ŧ���������� fade
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
                case "C2-Alone-1":
                case "askbf":
                case "askcake":
                case "asklib":
                case "extraterrestrial":
                case "C2-1S-1":
                case "C2-1S-2":
                case "C2-HellStop-1":
                case "C2-HellWorld-1":
                case "nameless":
                case "C2-1nameless":
                case "C2-Second-1":
                case "C2-Together-1":
                    newMusic = Resources.Load<AudioClip>("sound/lofi");
                    break;
                case "C2-Broadcast-1":
                    newMusic = Resources.Load<AudioClip>("sound/Brownnoise 2");
                    break;
                case "C4M":
                
                case "C4NS":
                    newMusic = Resources.Load<AudioClip>("sound/Sound -Special");
                    break;
                case "C5-Main": // ���͡óռԴ��Ҵ
                    newMusic = Resources.Load<AudioClip>("sound/Brownnoise 2");
                    break;
                // ���������� ������
                default:
                    newMusic = null; // �ҡ��辺�ŧ����Ѻ Scene ��� ����駤���� null
                    break;
            }

            // �ҡ���ŧ���ǡѹ�Ѻ�����ѧ������� ����ͧ������
            if (newMusic != null && currentClip != newMusic)
            {
                currentClip = newMusic;

                // �ҡ���� "C2-Broadcast-1" ��駤�����§�� 0.23
                if (sceneName == "C2-Broadcast-1")
                {
                    audioSource.volume = 0.23f; // ��駤�����§
                }
                else
                {
                    audioSource.volume = PlayerPrefs.GetFloat("volume", 1); // ��Ŵ�дѺ���§
                }

                PlayBackgroundMusic(); // ����ŧ����Ẻ���� ���� fade
            }
            else if (newMusic == null)
            {
                // �ҡ������ŧ����Ѻ Scene ��� �����ش���§
                audioSource.Stop(); // ��ش���§
                currentClip = null; // ���� currentClip
            }
        }
    }

    private IEnumerator PlayAndFadeBothMusic(AudioClip menuClip, AudioClip brownNoiseClip, float duration)
    {
        // ��駤�� AudioSource ��ѡ����Ѻ����ŧ menu
        audioSource.clip = menuClip;
        audioSource.loop = true;
        audioSource.volume = 1f; // ��������§ menu ����дѺ���
        audioSource.Play();

        // ��駤�� secondaryAudioSource ����Ѻ����ŧ Brownnoise 2
        secondaryAudioSource.clip = brownNoiseClip;
        secondaryAudioSource.loop = true;
        secondaryAudioSource.volume = 0f; // ��������§ brownnoise ����дѺ 0
        secondaryAudioSource.Play();

        float timer = 0f;

        // �ӡ�� fade ����ͧ���§������ѹ
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            // ���� � Ŵ���§�ͧ menu ŧ
            audioSource.volume = Mathf.Lerp(1f, 0f, progress);

            // ���� � �������§�ͧ Brownnoise 2 ���
            secondaryAudioSource.volume = Mathf.Lerp(0f, 1f, progress);

            yield return null;
        }

        // ����� fade �������, ����� Brownnoise 2 �ѧ������ ��лԴ���§ menu
        audioSource.volume = 0f; // �Դ���§ menu
        secondaryAudioSource.volume = 1f; // ���§ Brownnoise 2 �ѧ������
    }

    private void PlayBackgroundMusic()
    {
        if (audioSource != null && currentClip != null)
        {
            audioSource.clip = currentClip; // ��˹���Ի�ŧ
            audioSource.loop = true; // ������ŧ��蹫��
            if (!audioSource.isPlaying) // ��Ǩ�ͺ������§������������
            {
                audioSource.Play(); // ���������ŧ
            }
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
