using System;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;

public class ClientManager : MonoBehaviour
{
    public static ClientManager Instance;
    public StateTrade stateGame = StateTrade.Idle;
    private WebSocket ws;
    public float timeWait = 5;
    public float count = 0;
    
    private void Awake()
    {
        Instance = this;
    }

    public void InitConnection(string serverURL)
    {
        ws = new WebSocket(serverURL);
        
        ws.OnOpen += (sender, e) =>
        {
            GameManager.instance.HandelConnection(serverURL);
        };

        ws.OnMessage += (sender, e) =>
        {
            try
            {
                ResponsePacket packetResponse = JsonUtils.FromJson<ResponsePacket>(e.Data);
                Debug.Log(packetResponse.callbackResult);
                // Xử lý dữ liệu theo packetType
                switch (packetResponse.typeResponse)
                {
                    case TypeResponse.RESPONSE_REGISTER_TRUE:
                        GameManager.instance.HandelRegisterTrue();
                        
                        break;
                    
                    case TypeResponse.RESPONSE_REGISTER_FALSE:
                        GameManager.instance.HandleRegisterFail(packetResponse.callbackResult);
                        break;
                    
                    case TypeResponse.RESPONSE_LOGIN_TRUE:
                        GameManager.instance.HandelLoginTrue();
                        break;
                    
                    case TypeResponse.RESPONSE_LOGIN_FALSE:
                        GameManager.instance.HandleLoginFail(packetResponse.callbackResult);
                        break;
                    
                    case TypeResponse.RESPONSE_LOGOUT_TRUE:
                        GameManager.instance.HandleLogoutTrue();
                        break;
                    
                    case TypeResponse.RESPONSE_LOGOUT_FALSE:
                        //Todo logout false
                        break;
                    
                    case TypeResponse.RESPONSE_REGISTER_NAME_TRUE:
                        GameManager.instance.HandleRegisterNameTrue(packetResponse.callbackResult);
                        break;
                    
                    case TypeResponse.RESPONSE_REGISTER_NAME_FALSE:
                        GameManager.instance.HandleRegisterNameFail(packetResponse.callbackResult);
                        break;
                    
                    case TypeResponse.RESPONSE_GET_DATA_PLAYER:
                        GameManager.instance.HandleGetDataPlayer(packetResponse.playerInfo);
                        break;
                    
                    case TypeResponse.RESPONSE_GET_DATA_SHOP:
                        GameManager.instance.HandleGetDataShop(packetResponse.updateStoreData);
                        break;
                    
                    case TypeResponse.RESPONSE_MESSAGE:
                        GameManager.instance.HandleMessagePlayer(packetResponse.chatMessage);
                        break;
                }
            }
            catch (JsonException jsonEx)
            {
                Debug.LogError("Lỗi khi deserialize JSON: " + jsonEx.Message);
                //GameManager.instance.HandleFailDeserializeJson();
            }
            catch (Exception ex)
            {
                Debug.LogError("Lỗi xảy ra trong OnMessage: " + ex.Message);
                //GameManager.instance.HandleFailGetMessage();
            }
            
        };


        ws.OnError += (sender, e) =>
        {
            Debug.LogError("WebSocket Error: " + e.Message);
            //GameManager.instance.HandleFailConnection();
        };

        ws.OnClose += (sender, e) =>
        {
            Debug.Log("Connection closed: " + e.Reason);
        };
        
        ws.Connect();
        
        
    }
    
    // Đóng gói tập tin và chuẩn bị gửi
    public void HandelDataAndSend(RequestPacket1 requestPacket1, bool isLock)
    {
        if (isLock)
        {
            if (stateGame == StateTrade.Waiting)
            {
                Debug.Log("Waiting for request");
                return;
            }
            stateGame = StateTrade.Waiting;
        }
        SendDataToServer(RequestPacket1.toJson(requestPacket1));
    }
    public void HandelDataAndSend(RequestPacket requestPacket, bool isLock)
    {
        if (isLock)
        {
            if (stateGame == StateTrade.Waiting)
            {
                Debug.Log("Waiting for request");
                return;
            }
            stateGame = StateTrade.Waiting;
        }
        SendDataToServer(JsonUtils.ToJson(requestPacket));
    }

    // Gửi dữ liệu JSON đến server qua WebSocket
    private void SendDataToServer(string jsonData)
    {
        if (ws.IsAlive)
        {
            ws.Send(jsonData);
        }
        else
        {
            Debug.LogError("WebSocket is not open. Cannot send data.");
        }
    }

    private void OnApplicationQuit()
    {
        if (ws != null && ws.IsAlive)
        {
            ws.Close();
        }
    }
}
