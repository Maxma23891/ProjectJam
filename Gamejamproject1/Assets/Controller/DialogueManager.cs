
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
    public AudioSource SoundEffectSource;

    public GameController GameController;

    [SerializeField] private float waitTime = 0.05f;
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
        if (scene.background != null)
        {
            background.sprite = scene.background;
        }
        sentenceIndex = -1;
        PlayNextSentence();
    }

    public void PlayNextSentence()
    {
        GameObject obj = Instantiate(textBox, textBoxParent);
        
        TMP_Text textComponent = obj.transform.GetChild(1).GetComponent<TMP_Text>();

        StoryScene.Sentence currentSentence = currentScene.sentences[++sentenceIndex];
        textComponent.color = currentSentence.textColor;
        textComponent.fontSize = currentSentence.fontSize;

        TMP_Text speakerText = obj.transform.GetChild(0).GetComponent<TMP_Text>();
        speakerText.text = currentSentence.speaker.speakerName;
        speakerText.color = currentSentence.speaker.textColor;

        if(currentSentence.addTranslator){
            currentSentence.text = translator();
        }

        if(currentSentence.isRNG){
            int random = Random.Range(0, currentSentence.RNGText.Count);
            List<string> randomText = currentSentence.RNGText;
            StartCoroutine(TypeText(randomText[random], textComponent));
        }
        else{
            StartCoroutine(TypeText(currentSentence.text, textComponent));
        }
    }

    public bool IsCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool IsLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    private IEnumerator TypeText(string text, TMP_Text textComponent)
    {
        state = State.PLAYING;
        textComponent.text = "";  // Clear previous text
        int wordIndex = 0;

        // Type each character with a delay
        while (wordIndex < text.Length)
        {
            textComponent.text += text[wordIndex++];
            yield return new WaitForSeconds(waitTime);
        }

        state = State.COMPLETED;

        // If the current scene has choices, show them
        if (currentScene.isChoice && IsLastSentence())
        {
            ShowChoice();
        }
    }

    private void ShowChoice()
    {
        foreach (StoryScene.Choice choices in currentScene.choiceText)
        {
            GameObject obj = Instantiate(choiceButton, choiceParent);
            obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = choices.Text;
            obj.GetComponent<Button>().onClick.AddListener(() => MakeChoice(choices.Key, 1, choices.choiceScene));
        }
    }

    private void RandomChoice(StoryScene scene){
        bool isCorrect = Random.value > 0.5f;
        Debug.Log($"Random = {isCorrect}");
        if (isCorrect)
        {
            GameController.nextChoiceScene(scene);
        }
        else{
            GameController.nextChoiceScene(currentScene);
        }
        
    }

    private void MakeChoice(string key, int value, StoryScene scene)
    {
        ChoiceManager.Instance.SetChoice(key, value); // Store the choice
        Debug.Log($"Choice made: {key} = {PlayerPrefs.GetInt(key)}");
        Debug.Log(scene.isChoiceRNG);
        if(currentScene.isChoiceRNG){
            RandomChoice(scene);
        }
        else{
            GameController.nextChoiceScene(scene);
        }

        for (int i = choiceParent.childCount - 1; i >= 0; i--)
        {
            Destroy(choiceParent.GetChild(i).gameObject);
        }
    }

    private string translator(){
        if(currentScene.AudioTranslate != null){
            SoundEffectSource.clip = currentScene.AudioTranslate;
            SoundEffectSource.Play();
        }
        string result = RChar(2) + "w" + RChar(7)+"e"+ RChar(1) + " " + RChar(8) + "t" + RChar(2) + "a" + RChar(8) + "I" + RChar(1) + "k" + RChar(8) + "e" + RChar(2) + "d" + RChar(8) + " " + RChar(4) + "a" + RChar(5) + "g" + RChar(9) + "a" + RChar(0) + "e" + RChar(2) + "e" + RChar(8) + "n" + RChar(7) + " " + RChar(4) + "I" + RChar(7) + "O" + RChar(1) + "n" + RChar(3) + "g" + RChar(5) + "/." + RChar(2);
        return result;
    }
    

    private string RChar(int num)
    {
        string characters = "abcdefghijklmnopqrstuvwxyz"; // You can adjust the character set if needed
        string result = "";
        for(int i=0;i<num;i++){
            result += characters[Random.Range(0,26)];
        }
        return result;
    }
}

