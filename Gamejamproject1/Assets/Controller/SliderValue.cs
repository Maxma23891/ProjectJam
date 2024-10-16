using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecoratedSliderText : MonoBehaviour
{
    public Slider slider; // ��� Slider
    public TextMeshProUGUI handleText; // ��ͤ�������Ţ�� Handle

    void Start()
    {
        // ��駤�� Slider ����դ�� 0 - 1
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.value = 0.5f;

        // ��Ѻ Slider ���ͺʹͧ�������¹�ŧ���
        slider.onValueChanged.AddListener(UpdateHandleText);

        // �Ѿവ����Ţ� Handle
        UpdateHandleText(slider.value);
    }

    void UpdateHandleText(float value)
    {
        // �ʴ���Ңͧ Slider �� Handle ������ȹ��� 3 ���˹�
        handleText.text = value.ToString("0.000");

        // ���觢�ͤ���
        DecorateText(handleText);
    }

    void DecorateText(TextMeshProUGUI text)
    {
        text.color = Color.white; // ��駤���յ���ѡ�����բ��

        // ���ҧ��ͺ�մ��ͺ����ѡ��
        text.fontSharedMaterial = new Material(text.fontSharedMaterial); // ���ҧ Material ����
        text.fontSharedMaterial.SetColor("_OutlineColor", Color.black); // ��駤���ա�ͺ���մ�
        text.fontSharedMaterial.SetFloat("_OutlineWidth", 0.2f); // ��駤�Ҥ���˹Ңͧ��ͺ
    }
}
