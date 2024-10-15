using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    // ชื่อตัวแปรนี้เก็บชื่อของฉากเมนูหลัก
    public string menuSceneName = "MainMenu";  // เปลี่ยนชื่อตามฉากเมนูของคุณใน Unity

    // ฟังก์ชันนี้จะถูกเรียกเมื่อกดปุ่ม
    public void GoToMenu()
    {
        // โหลดฉากเมนูหลักโดยอ้างอิงตามชื่อที่ตั้งไว้
        SceneManager.LoadScene(menuSceneName);
    }
}
