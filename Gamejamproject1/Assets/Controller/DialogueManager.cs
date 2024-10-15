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
            "ฉัน : อีกครึ่งนึงเป็นของเธอเหมือนเดิมแล้วกันนะ",
            "เอ๊ะ?",
            "ฉัน : เรามาเจอกันที่คาเฟ่แห่งนี้ทุกวันเสาร์ ที่โต๊ะชิดผนังตัวนั้นซึ่งตั้งอยู่ข้างหน้าต่าง",
            "เขา : หรือว่าจะห่อกลับบ้านดี อืม...วันนี้เธอต้องรีบกลับไปทำงานรึเปล่า?",
            "ฉัน : อาจจะ แต่ไม่เป็นไรหรอก ยังพอมีเวลาอยู่บ้าง",
            "เขา : ดีจัง...",
            "ฉัน : ส่วนเรื่องห่อกลับบ้าน เหมือนว่าตอนนี้กล่องของที่ร้านน่าจะหมดพอดีน่ะ",
            "เขา : งั้นเหรอ?",
            "ฉัน : อือ เท่าที่เห็นนะ",
            "กลิ่นหอมของช็อกโกแลตลอยอยู่ในอากาศ ในร้านมีเสียงจอแจของลูกค้าและพนักงานที่พลุกพล่านอยู่ตลอดเวลา",
            "เธอเคยพูดว่าเสียงของผู้คนคือความเงียบในรูปแบบที่แตกต่าง",
            "ฉัน : เค้กติดแก้มอยู่น่ะ",
            "เขา : โอ๊ะ จริงเหรอ?",
            "ฉัน : ให้ช่วยเช็ดออกไหม?",
            "เขา : ขอบคุณนะ",
            "เขา : จะว่าไป พอพูดถึงเค้กแล้ว...",
            "เขา : เธอคิดว่าควรทำยังไงกับมันดี?",
            "ฉัน : ..."
        };

        currentDialogueIndex = 0;
        isWaiting = false;
        ShowNextSentence();
    }

    private void Update()
    {
        // เมื่อคลิกเมาส์หรือกดคีย์บอร์ด
        if ((Input.GetMouseButtonDown(0) || Input.anyKeyDown) && !isWaiting)
        {
            // ถ้าข้อความแสดงครบแล้ว ให้แสดงประโยคทั้งหมดทันที
            StopAllCoroutines(); // หยุดการทำงานของ coroutine อื่น ๆ
            dialogueText.text = dialogues[currentDialogueIndex]; // แสดงประโยคทั้งหมดทันที
            isWaiting = true; // เริ่มรอ
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
            yield return new WaitForSeconds(0.05f); // พิมพ์ทีละตัวอักษร
        }
    }

    private IEnumerator WaitAndShowNextSentence()
    {
        yield return new WaitForSeconds(1.25f); // รอ 1.25 วินาที
        isWaiting = false; // ยกเลิกการรอ
        ShowNextSentence(); // แสดงข้อความถัดไป
>>>>>>> Stashed changes
    }
}
