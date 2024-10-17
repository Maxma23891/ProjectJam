using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryScene", menuName = "Data/New StoryScene")]
[System.Serializable]
public class StoryScene : ScriptableObject
{
    public List<Sentence> sentences;
    public Sprite background;
    public StoryScene nextScene;
    public string changeScene;
    public bool isChoice;
    public bool isChoiceRNG;
    public List<Choice> choiceText;
    public AudioClip AudioTranslate;

    [System.Serializable]

    public struct Choice
    {
        public string Text;
        public string Key;
        public StoryScene choiceScene;
    }

    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public Speaker speaker;
        public Color textColor;  // Color for the sentence text
        public int fontSize;     // Font size for the sentence text
        public bool addTranslator;
        public bool isRNG;
        public List<string> RNGText;
        // Constructor to set default values
        public Sentence(string text, Speaker speaker)
        {
            this.text = text;
            this.speaker = speaker;
            this.textColor = Color.white;  // Default color is white
            this.fontSize = 17;            // Default font size (can adjust as needed)
            this.isRNG = false;            // Default to no RNG
            this.RNGText = new List<string>();  // Initialize with an empty list
            this.addTranslator = false;
        }
    }

    // Ensure all sentences have default colors and font sizes if not set in the inspector
    private void OnEnable()
    {
        for (int i = 0; i < sentences.Count; i++)
        {
            // Fetch the sentence from the list
            var sentence = sentences[i];

            // Apply default values if needed
            if (sentence.textColor == default)
                sentence.textColor = Color.white;
            
            if(sentence.textColor.a == 175){
                sentence.textColor = Color.white;
                sentence.textColor.a = 122;
            }
            

            if (sentence.fontSize <= 0)
                sentence.fontSize = 17;
            if(sentence.fontSize == 20){
                sentence.fontSize = 17;
            }
            // Write the modified sentence back to the list
            sentences[i] = sentence;
        }
    }

    private string ColorToHex(Color color)
    {
        Color32 color32 = color; // Convert to Color32 for easier conversion
        return $"#{color32.r:X2}{color32.g:X2}{color32.b:X2}{color32.a:X2}";
    }

}
