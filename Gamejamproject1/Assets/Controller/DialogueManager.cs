<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Image background;
    public Transform choiceParent;
    public GameObject choiceButton;
    public Transform textBoxParent;
    public GameObject textBox;

    public GameController GameController;

    [SerializeField] private float waittime = 0.05f;
    private int sentenceIndex = -1;
    private StoryScene currentScene;
    private State state = State.COMPLETED;

    private enum State
    {
        PLAYING, COMPLETED
    }

    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        if (scene.background != null){
            background.sprite = scene.background;
        }
        sentenceIndex = -1;
        PlayNextSentence();
    }

    public void PlayNextSentence()
    {
        GameObject obj = Instantiate(textBox,textBoxParent);
        StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text, obj));
        obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = currentScene.sentences[sentenceIndex].speaker.textColor;
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    private IEnumerator TypeText(string text,GameObject TextBox)
    {
        state = State.PLAYING;
        int wordIndex = 0;

        while (state != State.COMPLETED)
        {
            TextBox.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text += text[wordIndex];
            yield return new WaitForSeconds(waittime);
            if (++wordIndex == text.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
        if (currentScene.isChoice){
            ShowChoice();
        }
    }

    private void ShowChoice(){
        foreach (StoryScene.Choice choices in currentScene.choiceText){
            GameObject obj = Instantiate(choiceButton, choiceParent);
            obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = choices.Text;
            obj.GetComponent<Button>().onClick.AddListener(() => MakeChoice(choices.Key, 1, choices.choiceScene));
        }
    }
    private void MakeChoice(string key, int value,StoryScene scene)
    {
        ChoiceManager.Instance.SetChoice(key, value); // Store the choice
        Debug.Log($"Choice made: {key} = {PlayerPrefs.GetInt(key)}");
        foreach (Transform child in choiceParent)
        {
            Destroy(child.gameObject);
        }
        GameController.nextChoiceScene(scene);
=======
using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueText;

    private string[] dialogues;
    private int currentDialogueIndex;
    private bool isWaiting;

    private void Start()
    {
        dialogues = new string[]
        {
            "---",
            "�ѹ : �ա���觹֧�繢ͧ������͹������ǡѹ��",
            "����?",
            "�ѹ : ������͡ѹ��������觹��ء�ѹ����� �����ЪԴ��ѧ��ǹ�鹫�觵�������ҧ˹�ҵ�ҧ",
            "�� : ������Ҩ���͡�Ѻ��ҹ�� ���...�ѹ����͵�ͧ�պ��Ѻ价ӧҹ������?",
            "�ѹ : �Ҩ�� �����������͡ �ѧ�������������ҧ",
            "�� : �ըѧ...",
            "�ѹ : ��ǹ����ͧ��͡�Ѻ��ҹ ����͹��ҵ͹�����ͧ�ͧ�����ҹ��Ҩ�����ʹչ��",
            "�� : �������?",
            "�ѹ : ��� ��ҷ����繹�",
            "��������ͧ��͡��ŵ���������ҡ�� ���ҹ�����§��ᨢͧ�١�����о�ѡ�ҹ����ء���ҹ�����ʹ����",
            "���¾ٴ������§�ͧ��餹��ͤ�����º��ٻẺ���ᵡ��ҧ",
            "�ѹ : �页Դ���������",
            "�� : ���� ��ԧ����?",
            "�ѹ : ���������͡���?",
            "�� : �ͺ�س��",
            "�� : ������ �;ٴ�֧������...",
            "�� : �ͤԴ��Ҥ�÷��ѧ䧡Ѻ�ѹ��?",
            "�ѹ : ..."
        };

        currentDialogueIndex = 0;
        isWaiting = false;
        ShowNextSentence();
    }

    private void Update()
    {
        // ����ͤ�ԡ��������͡��������
        if ((Input.GetMouseButtonDown(0) || Input.anyKeyDown) && !isWaiting)
        {
            // ��Ң�ͤ����ʴ��ú���� ����ʴ�����¤�������ѹ��
            StopAllCoroutines(); // ��ش��÷ӧҹ�ͧ coroutine ��� �
            dialogueText.text = dialogues[currentDialogueIndex]; // �ʴ�����¤�������ѹ��
            isWaiting = true; // �������
            StartCoroutine(WaitAndShowNextSentence());
        }
    }

    private void ShowNextSentence()
    {
        if (currentDialogueIndex < dialogues.Length)
        {
            currentDialogueIndex++;
            if (currentDialogueIndex < dialogues.Length)
            {
                StartCoroutine(TypeSentence(dialogues[currentDialogueIndex]));
            }
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); // �������е���ѡ��
        }
    }

    private IEnumerator WaitAndShowNextSentence()
    {
        yield return new WaitForSeconds(1.25f); // �� 1.25 �Թҷ�
        isWaiting = false; // ¡��ԡ�����
        ShowNextSentence(); // �ʴ���ͤ����Ѵ�
>>>>>>> Stashed changes
    }
}
