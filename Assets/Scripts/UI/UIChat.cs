using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIChat : MonoBehaviour
{
    private RectTransform rect;
    private float lastMessageTime = 0f; // Thời gian gửi tin nhắn lần trước
    private float messageCooldown = 1f; // Thời gian chờ giữa 2 lần gửi (1 giây)
    
    private void Awake()
    {
        rect =  content.gameObject.GetComponent<RectTransform>();
    }

    public UIPrefabMessage prefabMessage;
    public Transform content;
    public TMP_InputField messageField;
    public Scrollbar scrollbarVertical;
    List<UIPrefabMessage> messages = new List<UIPrefabMessage>();
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAddMessageSelf();
        }
    }

    public void CheckAddMessageOpponent(string namePlayer, string mesage)
    {
        AddChatOpponent(namePlayer, mesage);
    }
    
    public void CheckAddMessageSelf()
    {
        float currentTime = Time.time;
        // Kiểm tra xem có ít nhất 1 giây giữa 2 lần gửi không
        if (currentTime - lastMessageTime < messageCooldown)
        {
            AddChatError("Nhắn chậm một chút nhen");
            return;
        }

        // Cập nhật thời gian gửi tin nhắn lần này
        lastMessageTime = currentTime;

        string txtMessage = messageField.text.Trim();
        if (txtMessage.Length >= 48)
        {
            AddChatError("Bạn không được gửi quá 48 kí tự");
        }
        else if (txtMessage == "")
        {
            AddChatError("Không thể gửi khoảng trống");
        }
        else
        {
            AddChatSefl(txtMessage);
            messageField.text = "";
            messageField.ActivateInputField();
        }
    }
    public void ClearListMessages()
    {
        if (messages.Count > 0)
        {
            foreach (UIPrefabMessage prefabMessage in messages) 
            {
                Destroy(prefabMessage.gameObject);
            }
            messages.Clear();
            UpdateSizeContent();
        }
    }
    void AddChatOpponent(string nameOpponent, string value)
    {
        UIPrefabMessage newMessage = Instantiate(this.prefabMessage, content);
        messages.Add(newMessage);
        newMessage.InitMessage(PlayerRole.Opponent, Color.green, nameOpponent, value);
        UpdateSizeContent();
    }

    Color GetRandomColor()
    {
        int random = Random.Range(0, 5);
        if(random == 0) return Color.green;
        else if(random == 1) return Color.red;
        else if(random == 2) return Color.magenta;
        else if(random == 3) return Color.gray;
        else return Color.blue;
    }
    void AddChatSefl(string value)
    {
        UIPrefabMessage newMessage = Instantiate(this.prefabMessage, content);
        messages.Add(newMessage);
        
        string namePlayer = Player.instance.NamePlayer;
        newMessage.InitMessage(PlayerRole.Self, Color.yellow, namePlayer, value);
        
        GameManager.instance.RequestMessage(namePlayer, value);

        UpdateSizeContent();
    }

    void AddChatError(string messageError)
    {
        UIPrefabMessage newMessage = Instantiate(this.prefabMessage, content);
        messages.Add(newMessage);
        
        string namePlayer = Player.instance.NamePlayer;
        newMessage.InitMessage(PlayerRole.SelfError, Color.red, namePlayer, messageError);

        UpdateSizeContent();
    }
    void UpdateSizeContent()
    {
        if (messages.Count <= 10)
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 400);
            scrollbarVertical.value = 1;
        }
        else
        {
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, 40 * messages.Count);
            scrollbarVertical.value = 0;
        }
    }

    public void OnChat()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("OnChat: Đối tượng không có RectTransform.");
            return;
        }

        // Lưu lại vị trí hiện tại (đích đến)
        Vector2 targetPosition = rectTransform.anchoredPosition;

        // Đặt vị trí ban đầu ở ngoài màn hình (bên trái)
        Vector2 offScreenPosition = new Vector2(rectTransform.rect.width, rectTransform.anchoredPosition.y);
        rectTransform.anchoredPosition = offScreenPosition;

        // Bật đối tượng
        gameObject.SetActive(true);

        // Lướt vào vị trí hiện tại
        rectTransform.DOAnchorPos(targetPosition, 1f).SetEase(Ease.OutExpo);
    }

}
