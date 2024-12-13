using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIChat : MonoBehaviour
{
    public static UIChat instance;

    private void Awake()
    {
        instance = this;
    }

    public UIPrefabMessage prefabMessage;
    public Transform content;
    public TMP_InputField messageField;
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
        string txtMessage = messageField.text;
        if (txtMessage.Length >= 36)
        {
            Debug.Log("Quá dài");
        }
        else
        {
            AddChatSefl(txtMessage);
            messageField.text = "";
        }
    }
    public void ClientList()
    {
        foreach (UIPrefabMessage prefabMessage in messages) 
        {
            Destroy(prefabMessage.gameObject);
        }
        messages.Clear();
    }
    void AddChatOpponent(string nameOpponent, string value)
    {
        UIPrefabMessage newMessage = Instantiate(this.prefabMessage, content);
        newMessage.InitMessage(PlayerRole.Opponent, GetRandomColor(), nameOpponent, value);
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
        string namePlayer = Player.instance.NamePlayer;
        newMessage.InitMessage(PlayerRole.Self, Color.yellow, namePlayer, value);
        GameManager.instance.RequestMessage(namePlayer, value);
    }
}
