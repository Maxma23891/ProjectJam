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

    IEnumerator LoadLevel(string LoadName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(LoadName);
    }
}
