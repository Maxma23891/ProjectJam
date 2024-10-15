
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

        StartCoroutine(TypeText(currentSentence.text, textComponent));
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
        if (currentScene.isChoice)
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
    private void MakeChoice(string key, int value, StoryScene scene)
    {
        ChoiceManager.Instance.SetChoice(key, value); // Store the choice
        Debug.Log($"Choice made: {key} = {PlayerPrefs.GetInt(key)}");
        GameController.nextChoiceScene(scene);
        for (int i = choiceParent.childCount - 1; i >= 0; i--)
        {
            Destroy(choiceParent.GetChild(i).gameObject);
        }
    }
}

