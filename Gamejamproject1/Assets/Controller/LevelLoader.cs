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
        SceneManager.LoadScene(PlayerPrefs.GetInt("Chapter", 2));
    }

    IEnumerator LoadLevel(string LoadName)
    {
        transition.SetTrigger("Start");
        yield return null;
        // yield return new WaitForSeconds(0f);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Chapter", 2);
        PlayerPrefs.Save();
        SceneManager.LoadScene(LoadName);
    }


}
