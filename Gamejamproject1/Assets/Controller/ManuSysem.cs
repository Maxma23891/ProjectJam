using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManuSysem : MonoBehaviour
{
    public string SceneName;
    
    private void Start()
    {

    }

    public void ClickStart()
    {
        StartCoroutine(Delaytime(SceneName));
    }

    public void ClickExit()
    {
        Application.Quit();
    }

    IEnumerator Delaytime(string LoadName)
    {
        SceneManager.LoadScene(LoadName);
        yield return new WaitForSeconds(2f);
    }
}
