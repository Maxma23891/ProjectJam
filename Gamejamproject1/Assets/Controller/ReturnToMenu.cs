using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    // ���͵���ù���纪��ͧ͢�ҡ������ѡ
    public string menuSceneName = "MainMenu";  // ����¹���͵���ҡ���٢ͧ�س� Unity

    // �ѧ��ѹ���ж١���¡����͡�����
    public void GoToMenu()
    {
        // ��Ŵ�ҡ������ѡ����ҧ�ԧ������ͷ�������
        SceneManager.LoadScene(menuSceneName);
    }
}
