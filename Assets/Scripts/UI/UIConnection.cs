using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIConnection : MonoBehaviour
{
    public TMP_InputField inputIP;
    public TMP_InputField inputPort;
    public Button connectButton;
    public TextMeshProUGUI statusText;
    private float countTimeStatus = 0;

    private void Update()
    {
        if (countTimeStatus > 0)
        {
            countTimeStatus -= Time.deltaTime;
        }

        if (countTimeStatus <= 0)
        {
            countTimeStatus = 0;
            statusText.text = "";
        }
    }

    void ConnectServer()
    {
        string ip = inputIP.text.Trim();
        string port = inputPort.text.Trim();

        // Validate IP and port
        if (!IsValidIP(ip) || !IsValidPort(port))
        {
            SetStatusText("Địa chỉ IP hoặc Cổng không hợp lệ", Color.yellow);
            return;
        }

        string connectURL = $"ws://{ip}:{port}";
        SetStatusText("Không thể kết nối đến máy chủ", Color.red);
        try
        {
            ClientManager.Instance.InitConnection(connectURL);
        }
        catch (Exception ex)
        {
            SetStatusText("Có lỗi xảy ra, vui lòng thử lại", Color.red);
        }
    }

// Helper method for IP validation
    bool IsValidIP(string ip)
    {
        return System.Net.IPAddress.TryParse(ip, out _);
    }

// Helper method for port validation
    bool IsValidPort(string port)
    {
        if (int.TryParse(port, out int portNumber))
        {
            return portNumber > 0 && portNumber <= 65535;
        }
        return false;
    }

    void SetStatusText(string text, Color colorText)
    {
        statusText.text = text;
        statusText.color = colorText;
        countTimeStatus = 3;
    }
    public void OnConnectUI()
    {
        CleanConnection();
        gameObject.SetActive(true);
        connectButton.onClick.AddListener(ConnectServer);
    }

    public void CloseConnectionUI()
    {
        CleanConnection();
        gameObject.SetActive(false);
    }

    void CleanConnection()
    {
        inputIP.text = "";
        inputPort.text = "";
        connectButton.onClick.RemoveAllListeners();
    }
}
