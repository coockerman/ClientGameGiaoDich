using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAuth : MonoBehaviour
{
    [SerializeField] TMP_InputField userNameRegister, passwordRegister, passwordRegisterRe;
    [SerializeField] TMP_InputField userNameLogin, passwordLogin;
    [SerializeField] GameObject registerPanel, loginPanel;
    [SerializeField] private Button loginBtn, registerBtn, onLoginBtn, onRegisterBtn, forgetPasswordBtn;
    [SerializeField] TextMeshProUGUI dialogRegister, dialogLogin;

    private float maxTimeWaitTxt = 5f;
    private float countWaitTxt = 0f;

    private void Start()
    {
        registerBtn.onClick.AddListener(PlayerRegister);
        loginBtn.onClick.AddListener(PlayerLogin);
        onLoginBtn.onClick.AddListener(ChangeOnLoginUI);
        onRegisterBtn.onClick.AddListener(ChangeOnRegisterUI);
        forgetPasswordBtn.onClick.AddListener(ForgetPassword);
    }

    private void Update()
    {
        if(countWaitTxt > 0) countWaitTxt-= Time.deltaTime;
        if (countWaitTxt < 0)
        {
            countWaitTxt = 0;
            dialogLogin.text = "";
            dialogRegister.text = "";
        }
    }

    public void UIRegisterSucess()
    {
        OffAuthUI();
        //UIManager.instance.uiRegisterName.OnRegisterNameUI();
    }
    public void UILoginSuccess()
    {
        OffAuthUI();
    }

    public void UIRegisterFailed(string dialog)
    {
        OnDialogRegister(dialog, Color.red);
    }
    public void UILoginFailed(string dialog)
    {
        OnDialogLogin(dialog, Color.red);
    }
    public void OnAuthUI()
    {
        gameObject.SetActive(true);
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }

    void OffAuthUI()
    {
        gameObject.SetActive(false);
    }

    void ForgetPassword()
    {
        if (userNameLogin.text != "")
        {
            OnDialogRegister("Đã gửi thông tin về email của bạn", Color.green);
            GameManager.instance.RequestForgetPassword(userNameLogin.text);
        }
        else
        {
            OnDialogRegister("Không được để trống", Color.red);
        }
        
        
    }
    void PlayerRegister()
    {
        string strNameRegister = userNameRegister.text;
        string strPassResgister = passwordRegister.text;
        string strPassResgisterRe = passwordRegisterRe.text;
        if (strNameRegister == "" || strPassResgister == "" || strPassResgisterRe == "")
        {
            OnDialogRegister("Không được để trống", Color.red);
            return;
        }

        if (strPassResgister.Length < 6)
        {
            OnDialogRegister("Mật khẩu ít nhất 6 kí tự", Color.yellow);
        }
        
        if (strPassResgister != strPassResgisterRe)
        {
            OnDialogRegister("Mật khẩu nhập lại không đúng", Color.red);
            return;
        }
        AuthData authData = new AuthData();
        authData.userName = strNameRegister;
        authData.password = strPassResgister;
        
        GameManager.instance.RequestRegister(strNameRegister, strPassResgister);
    }

    void PlayerLogin()
    {
        string strNameLogin = userNameLogin.text;
        string strPassLogin = passwordLogin.text;
        if (strNameLogin == "" || strPassLogin == "")
        {
            OnDialogLogin("Không được để trống", Color.red);
            return;
        }
        
        AuthData authData = new AuthData();
        authData.userName = strNameLogin;
        authData.password = strPassLogin;
        GameManager.instance.RequestLogin(strNameLogin, strPassLogin);
    }

    void ChangeOnRegisterUI()
    {
        CleanUI();
        registerPanel.gameObject.SetActive(true);
        loginPanel.gameObject.SetActive(false);
    }
    void ChangeOnLoginUI()
    {
        CleanUI();
        loginPanel.gameObject.SetActive(true);
        registerPanel.gameObject.SetActive(false);
    }
    void CleanUI()
    {
        dialogRegister.text = "";
        dialogLogin.text = "";
        
        userNameRegister.text = "";
        userNameLogin.text = "";
        passwordRegister.text = "";
        passwordRegisterRe.text = "";
        passwordLogin.text = "";
    }

    void OnDialogRegister(string dialog, Color color)
    {
        countWaitTxt = maxTimeWaitTxt;
        dialogRegister.text = dialog;
        dialogRegister.color = color;
    }
    void OnDialogLogin(string dialog, Color color)
    {
        countWaitTxt = maxTimeWaitTxt;
        dialogLogin.text = dialog;
        dialogLogin.color = color;
    }
}
