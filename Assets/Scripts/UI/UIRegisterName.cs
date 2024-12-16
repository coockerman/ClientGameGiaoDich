using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRegisterName : MonoBehaviour
{
    public TMP_InputField namePlayerInputField;
    public Button btnRegisterNamePlayer;

    private void Start()
    {
        btnRegisterNamePlayer.onClick.AddListener( RegisterNamePlayer );
    }

    void RegisterNamePlayer()
    {
        if (namePlayerInputField.text.Length > 3 && namePlayerInputField.text.Length < 16)
        {
            GameManager.instance.RequestRegisterPlayer(namePlayerInputField.text);
        }
        else
        {
            Debug.Log("Can't register name player");
        }
    }
}
