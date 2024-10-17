using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public StoryScene currentScene;
    public DialogueManager bottombar;
    public GameObject ContinueButton;
    
    void Start()
    {
        bottombar.PlayScene(currentScene);

        PlayerPrefs.SetInt("Chapter", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
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
                    checkNextScene();
                }
                else
                {
                    bottombar.PlayNextSentence();
                }

            }
        }

    }

    public void checkNextScene(){
        if (currentScene.nextScene is BranchScene branchScene)
        {
            currentScene = currentScene.nextScene;
            bool branchFound = false;

            // Check all branches for valid conditions
            foreach (BranchScene.Branch branch in branchScene.scenes)
            {
                if (AreConditionsMet(branch.keyConditions))
                {
                    Debug.Log($"Branch found: {branch.targetScene.name}");
                    currentScene = branch.targetScene;
                    bottombar.PlayScene(currentScene);
                    branchFound = true;
                    break;
                }
            }

            // If no branch is valid, load the default normal scene
            if (!branchFound)
            {
                Debug.Log("No valid branch found. Loading normal scene.");
                currentScene = branchScene.normalScene;
                bottombar.PlayScene(currentScene);
            }
        }
        else if (currentScene is StoryScene storyScene){
            if (storyScene.nextScene != null){
                currentScene = storyScene.nextScene;
                bottombar.PlayScene(currentScene);
            }
            else{
                ContinueButton.SetActive(true);
                ContinueButton.GetComponent<Button>().onClick.AddListener(() =>{
                    SceneManager.LoadScene(currentScene.changeScene);
                    if(SceneManager.GetActiveScene().buildIndex == 2){
                        PlayerPrefs.SetInt("Chapter", SceneManager.GetActiveScene().buildIndex + 1);
                        PlayerPrefs.Save();
                    }
                    });
            }
        }
    }

    public void nextChoiceScene(StoryScene nextscene){
        currentScene = nextscene;
        bottombar.PlayScene(currentScene);
    }

    private bool AreConditionsMet(List<string> keyConditions)
    {
        foreach (var key in keyConditions)
        {
            Debug.Log(PlayerPrefs.GetInt(key));
            if (ChoiceManager.Instance.GetChoice(key, 0) == 0)
            {
                return false;
            }
        }
        return true; // All conditions met
    }
}
