using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void GoToOptions()
    {
        SceneManager.LoadScene("option"); 
    }
}
