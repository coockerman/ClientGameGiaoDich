using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIInformation : MonoBehaviour
{
    public TextMeshProUGUI txtNamePlayer;
    public TextMeshProUGUI txtDayPlayer;

    public void Init(string namePlayer, int dayPlayer)
    {
        txtNamePlayer.text = namePlayer;
        txtDayPlayer.text = "Ngày: " + dayPlayer.ToString();
    }

    public void UpdateDayPlayer(int dayPlayer)
    {
        txtDayPlayer.text = "Ngày: " + dayPlayer.ToString();
    }

    public void OnUIInformation()
    {
        SlideFromTop(gameObject);
    }
    private void SlideFromTop(GameObject uiElement)
    {
        RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("SlideFromTop: Đối tượng không có RectTransform.");
            return;
        }

        // Lưu vị trí hiện tại (đích đến)
        Vector2 targetPosition = rectTransform.anchoredPosition;

        // Đặt vị trí ban đầu (trên màn hình, ngoài vùng nhìn thấy)
        Vector2 offScreenPosition = new Vector2(targetPosition.x, rectTransform.rect.height + Screen.height / 2);
        rectTransform.anchoredPosition = offScreenPosition;

        // Bật đối tượng
        uiElement.SetActive(true);

        // Tạo hiệu ứng trượt từ trên xuống
        rectTransform.DOAnchorPos(targetPosition, 0.5f).SetEase(Ease.OutBounce); // Trượt với hiệu ứng nảy nhẹ
    }
}
