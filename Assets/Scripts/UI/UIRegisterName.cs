using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRegisterName : MonoBehaviour
{
    public TMP_InputField namePlayerInputField;
    public Button btnRegisterNamePlayer;
    public TextMeshProUGUI dialogRegister;
    private float timeOnDialog = 0;
    private void Start()
    {
        btnRegisterNamePlayer.onClick.AddListener( RegisterNamePlayer );
    }

    private void Update()
    {
        if(timeOnDialog > 0) timeOnDialog -= Time.deltaTime;
        if (timeOnDialog < 0)
        {
            timeOnDialog = 0;
            dialogRegister.text = "";
        }
    }

    void RegisterNamePlayer()
    {
        string playerName = namePlayerInputField.text;

        // Kiểm tra độ dài
        if (playerName.Length < 5 || playerName.Length > 15)
        {
            SetTextDialogRegister("Tên dài từ 5 - 15 kí tự", Color.yellow);
            return;
        }

        // Kiểm tra khoảng trắng
        if (playerName.Contains(" "))
        {
            SetTextDialogRegister("Tên không được chứa khoảng trắng", Color.yellow);
            return;
        }

        // Nếu hợp lệ, gửi yêu cầu đăng ký
        GameManager.instance.RequestRegisterName(playerName);
    }


    public void SetTextDialogRegister(string textDialog, Color textColor)
    {
        dialogRegister.text = textDialog;
        dialogRegister.color = textColor;
        timeOnDialog = 3f;
    }

    public void OnRegisterNameUI()
    {
        gameObject.SetActive(true);
    }

    public void CloseUIRegister(float timeClose)
    {
        // Tạo hiệu ứng làm mờ CanvasGroup trước khi tắt
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.DOFade(0, timeClose).OnComplete(() => gameObject.SetActive(false));
        }
        else
        {
            // Nếu không có CanvasGroup, chỉ tắt sau một khoảng thời gian
            DOVirtual.DelayedCall(timeClose, () => gameObject.SetActive(false));
        }
    }
}
