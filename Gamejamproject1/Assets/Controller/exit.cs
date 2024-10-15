using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void ExitGame()
    {
        // แสดงข้อความใน Console
        Debug.Log("Exiting game...");

        // ถ้าในโหมด Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // หยุดการเล่นใน Editor
#else
            Application.Quit(); // ออกจากเกมเมื่อใน Build
#endif
    }
}
