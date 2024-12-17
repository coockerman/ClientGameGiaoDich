using System;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;

public class ClientManager : MonoBehaviour
{
    public static ClientManager Instance;
    public StateTrade stateGame = StateTrade.Idle;
    private WebSocket ws;
    
    public string serverURL = "ws://127.0.0.1:5555"; // Địa chỉ server WebSocket

    private void Awake()
    {
        Instance = this;
        ws = new WebSocket(serverURL);
        
        ws.OnOpen += (sender, e) =>
        {
            //RequestPacket packet = new RequestPacket(PacketType.UpdateStore);
            //HandelDataAndSend(packet);
        };

        ws.OnMessage += (sender, e) =>
        {
            try
            {
                RequestPacket packet = RequestPacket.fromJson(e.Data);
                // Xử lý dữ liệu theo packetType
                switch (packet.packetType)
                {
                    case PacketType.ResponseBuy:
                        GameManager.instance.HandleBuy(packet.abstractData);
                        stateGame = StateTrade.Idle;
                        break;
            
                    case PacketType.ResponseSell:
                        GameManager.instance.HandleSell(packet.abstractData);
                        stateGame = StateTrade.Idle;
                        break;
            
                    case PacketType.ResponseUpdateStore:
                        GameManager.instance.HandleUpdateStore(packet.updateStoreData);
                        break;
                    
                    case PacketType.ResponseFindOpponent:
                        
                        break;
                    case PacketType.ResponseNamePlayer:
                        GameManager.instance.HandleGetNamePlayer(packet.namePlayer);
                        break;
                    case PacketType.ResponseMessagePlayer:
                        GameManager.instance.HandleMessagePlayer(packet.namePlayer, packet.messagePlayer);
                        break;
                    case PacketType.ResponseRegisterPlayer:
                        GameManager.instance.HandleRegisterPlayer(packet.namePlayer, packet.isRegisterPlayer);
                        stateGame = StateTrade.Idle;
                        break;
                }
            }
            catch (JsonException jsonEx)
            {
                Debug.LogError("Lỗi khi deserialize JSON: " + jsonEx.Message);
            }
            catch (Exception ex)
            {
                Debug.LogError("Lỗi xảy ra trong OnMessage: " + ex.Message);
            }
        };


        ws.OnError += (sender, e) =>
        {
            //Todo error
            Debug.LogError("WebSocket Error: " + e.Message);
        };

        ws.OnClose += (sender, e) =>
        {
            
            Debug.Log("Connection closed: " + e.Reason);
        };
    }
    
    void Start()
    {
        ws.Connect();
    }
    
    // Đóng gói tập tin và chuẩn bị gửi
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
        SendDataToServer(RequestPacket.toJson(requestPacket));
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
