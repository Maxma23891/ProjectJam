using UnityEngine;

public class MouseClickSound : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ������ա�ä�ԡ����
        {
            // ���� SoundManager � Scene ������¡�� PlayClickSound
            SoundManager soundManager = FindObjectOfType<SoundManager>();
            if (soundManager != null)
            {
                soundManager.PlayClickSound();
            }
        }
    }
}
