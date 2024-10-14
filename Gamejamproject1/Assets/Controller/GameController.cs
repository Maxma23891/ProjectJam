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
                    NextScene();
                }
                else
                {
                    bottombar.PlayNextSentence();
                }

            }
        }

    }

    public void NextScene(){
        currentScene = currentScene.nextScene;
        bottombar.PlayScene(currentScene);
    }
}
