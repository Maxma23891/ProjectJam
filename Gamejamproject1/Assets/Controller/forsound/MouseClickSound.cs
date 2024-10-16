using UnityEngine;

public class MouseClickSound : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            
            SoundManager soundManager = FindObjectOfType<SoundManager>();
            if (soundManager != null)
            {
                soundManager.PlayClickSound();
            }
        }
    }
}
