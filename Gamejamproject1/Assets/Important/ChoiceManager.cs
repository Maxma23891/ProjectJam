using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    public static ChoiceManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetChoice(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    // Retrieve a choice value with a default fallback
    public int GetChoice(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }
}
