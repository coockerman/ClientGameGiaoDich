using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUpdatePassword : MonoBehaviour
{
     public TMP_InputField inputPasswordOld, inputPasswordNew, inputPasswordReNew;
     public TextMeshProUGUI dialogPass;
     public Button btnConfirm, btnExit;
     
     private float maxTimeWaitTxt = 5f;
     private float countWaitTxt = 0f;

     private void Start()
     {
         btnConfirm.onClick.AddListener(ChangePassword);
         btnExit.onClick.AddListener(OffUpdatePass);
     }

     private void Update()
     {
         if(countWaitTxt > 0) countWaitTxt-= Time.deltaTime;
         if (countWaitTxt < 0)
         {
             countWaitTxt = 0;
             dialogPass.text = "";
         }
     }

     void ChangePassword()
     {
         string passOld = inputPasswordOld.text;
         string passNew = inputPasswordNew.text;
         string passReNew = inputPasswordReNew.text;
         if (passOld == null || passNew == null || passReNew == null)
         {
             OnDialogUpdatePass("Không được để trống", Color.yellow);
             return;
         }
         if (passOld == passNew)
         {
             OnDialogUpdatePass("Mật khẩu mới không được giống mật khẩu cũ", Color.yellow);
             return;
         }
         if (passNew != passReNew)
         {
             OnDialogUpdatePass("Mật khẩu xác nhận không đúng", Color.yellow);
             return;
         }
         GameManager.instance.RequestResetPassword(passOld, passNew);
     }
     public void OnUpdatePass()
     {
          CleanUI();
          gameObject.SetActive(true);
     }
    
     public void UpdatePassTrue(string text)
     {
         OnDialogUpdatePass(text, Color.green);
     }
     public void UpdatePassFalse(string text)
     {
         OnDialogUpdatePass(text, Color.red);
     }
     void OffUpdatePass()
     {
         gameObject.SetActive(false);
     }
     void OnDialogUpdatePass(string dialog, Color color)
     {
         countWaitTxt = maxTimeWaitTxt;
         this.dialogPass.text = dialog;
     }
     void CleanUI()
     {
         inputPasswordOld.text = "";
         inputPasswordNew.text = "";
         inputPasswordReNew.text = "";
         dialogPass.text = "";
     }
 }
