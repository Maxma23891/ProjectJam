using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public StoryScene currentScene;
    public BottomBarController bottombar;
    
    void Start()
    {
        bottombar.PlayScene(currentScene);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (bottombar.IsCompleted() && !currentScene.isChoice)
            {
                if (bottombar.IsLastSentence())
                {
                    currentScene = currentScene.nextScene;
                    bottombar.PlayScene(currentScene);
                }
                else
                {
                    bottombar.PlayNextSentence();
                }

            }
        }

    }

    public void nextChoiceScene(StoryScene nextscene){
        currentScene = nextscene;
        bottombar.PlayScene(currentScene);
    }
}
