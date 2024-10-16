using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void ExitGame()
    {
        // แสดงข้อความใน Console
        Debug.Log("Exiting game...");


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); 
#endif
    }
}
