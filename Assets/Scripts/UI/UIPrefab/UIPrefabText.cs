using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPrefabText : MonoBehaviour
{
    public RectTransform targetObject; // Object cần thay đổi kích thước
    public TextMeshProUGUI textMeshPro;


    public void InitText(Color colorText, string text)
    {
        targetObject = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = text;
        textMeshPro.color = colorText;
        SetSize();
    }

    void SetSize()
    {
        if (textMeshPro.text != null)
        {
            Vector2 textSize = textMeshPro.GetPreferredValues(textMeshPro.text);

            // Cập nhật kích thước object dựa trên kích thước của text
            targetObject.sizeDelta = new Vector2(textSize.x + 5, textSize.y + 5); // Thêm padding nếu cần
        }
    }
}
