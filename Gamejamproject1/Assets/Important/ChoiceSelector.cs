using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSelector : MonoBehaviour
{
    public List<Choice> choices;
    public GameController GameController;


    [System.Serializable]

    public struct Choice
    {
        public Button bt;
        public string Key;
    }

    void Start()
    {
        foreach(Choice x in choices){
            x.bt.onClick.AddListener(() => MakeChoice(x.Key, 1));
        }
    }

    void MakeChoice(string key, int value)
    {
        ChoiceManager.Instance.SetChoice(key, value); // Store the choice
        Debug.Log($"Choice made: {key} = {value}");
        GameController.NextScene();
    }


}
