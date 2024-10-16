using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecoratedSliderText : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI handleText;

    void Start()
    {
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 0.5f;
        slider.onValueChanged.AddListener(UpdateHandleText);
        UpdateHandleText(slider.value);
    }

    void UpdateHandleText(float value)
    {
        handleText.text = value.ToString("0.000");
        DecorateText(handleText);
    }

    void DecorateText(TextMeshProUGUI text)
    {
        text.color = Color.white;
        text.fontSharedMaterial = new Material(text.fontSharedMaterial);
        text.fontSharedMaterial.SetColor("_OutlineColor", Color.black);
        text.fontSharedMaterial.SetFloat("_OutlineWidth", 0.2f);
    }
}
