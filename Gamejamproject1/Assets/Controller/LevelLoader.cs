using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public string SceneName;
    public  Animator transition;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickStart()
    {
        StartCoroutine(LoadLevel(SceneName));
    }

    public void ClickExit()
    {
        Application.Quit();
    }
    
    public void ContinueButtonClick(){
        SceneManager.LoadScene(PlayerPrefs.GetInt("Chapter", 1));
    }

    IEnumerator LoadLevel(string LoadName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("Chapter", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(LoadName);
    }


}
