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
    private float waittime = 0.05f;
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
        GameObject obj = Instantiate(textBox,textBoxParent);
        StartCoroutine(TypeText(currentScene.sentences[++sentenceIndex].text, obj));
        obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().color = currentScene.sentences[sentenceIndex].speaker.textColor;
    }

    private IEnumerator TypeText(string text,GameObject TextBox)
    {
        state = State.PLAYING;
        int wordIndex = 0;
        TextBox.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "";
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
    }

    public void Skip()
    {
        

        skipping = true;
        waittime = 0f; // Speed up typing

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
        waittime = 0.05f; // Reset typing speed
        Menu.SetActive(true); // Open menu when done
        if(PlayerPrefs.GetInt("Chapter",0) > 0){
            Menu.transform.GetChild(1).gameObject.SetActive(true);
        }
    }


}
