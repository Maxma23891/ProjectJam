using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void ExitGame()
    {
        // �ʴ���ͤ���� Console
        Debug.Log("Exiting game...");

        // �������� Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ��ش������� Editor
#else
            Application.Quit(); // �͡�ҡ�������� Build
#endif
    }
}
