
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

    private IEnumerator TypeText(string text, GameObject TextBox)
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
        foreach (Transform child in choiceParent)
        {
            Destroy(child.gameObject);
        }
        GameController.nextChoiceScene(scene);
    }
}

