using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    
    public string menuSceneName = "MainMenu";  

    
    public void GoToMenu()
    {
        
        SceneManager.LoadScene(menuSceneName);
    }
}
