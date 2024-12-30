using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class AuthData
{
    public WebSocket socket;
    public string userName;
    public string password;

    public AuthData() {
        
    }

    public AuthData(WebSocket socket, string userName, string password)
    {
        this.socket = socket;
        this.userName = userName;
        this.password = password;
    }
}
