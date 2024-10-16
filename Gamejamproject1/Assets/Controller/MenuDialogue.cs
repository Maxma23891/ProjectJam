using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuDialogue : MonoBehaviour
{
    public Transform textBoxParent;
    public GameObject textBox;
    public StoryScene currentScene;
    public GameObject Menu;
    private int sentenceIndex = -1;
    [SerializeField] private float waitTime = 0.05f;
    private State state = State.COMPLETED;
    private bool skipping = false;

    private enum State
    {
        PLAYING, COMPLETED
    }
    void Update()
    {
        if( !skipping){
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
                if(state == State.COMPLETED){
                    if(sentenceIndex + 1 == currentScene.sentences.Count){
                        Menu.SetActive(true);
                    }
                    else{
                        PlayNextSentence();
                    }
                }
            }
        }
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

    }

    public void Skip()
    {
        

        skipping = true;
        waitTime = 0f; // Speed up typing

        StartCoroutine(SkipAllSentences());
    }

    private IEnumerator SkipAllSentences()
    {
        yield return new WaitForSeconds(0.5f);
        if (state == State.PLAYING)
            yield break; // Prevent multiple skips at the same time
        while (sentenceIndex < currentScene.sentences.Count - 1)
        {
            PlayNextSentence();
            while (state == State.PLAYING) // Wait until the current sentence finishes
            {
                yield return null;
            }
        }

        skipping = false;
        waitTime = 0.05f; // Reset typing speed
        Menu.SetActive(true); // Open menu when done
        if(PlayerPrefs.GetInt("Chapter",0) > 1){
            Menu.transform.GetChild(1).gameObject.SetActive(true);
        }
    }


}
