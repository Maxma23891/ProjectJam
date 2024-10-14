using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryScene", menuName = "Data/New StoryScene")]
[System.Serializable]
public class StoryScene : ScriptableObject
{
    public List<Sentence> sentences;
    public Sprite background;
    public StoryScene nextScene;
    public bool isChoice;
    public List<Choice> choiceText;

    [System.Serializable]

    public struct Choice
    {
        public string Text;
        public string Key;
    }

    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public Speaker speaker;
    }
}
