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
        if (namePlayerInputField.text.Length > 4 && namePlayerInputField.text.Length < 16)
        {
            GameManager.instance.RequestRegisterPlayer(namePlayerInputField.text);
        }
        else
        {
            
            SetTextDialogRegister("Tên dài từ 5 - 15 kí tự", Color.yellow);
        }
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
