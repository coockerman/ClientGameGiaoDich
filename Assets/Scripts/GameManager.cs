using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Hàng đợi các hành động cần xử lý trên main thread
    private readonly Queue<Action> mainThreadActions = new Queue<Action>();

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        UIManager.instance.uiConnection.OnConnectUI();
    }

    private void Update()
    {
        // Xử lý tất cả các hành động trong hàng đợi
        while (mainThreadActions.Count > 0)
        {
            mainThreadActions.Dequeue()?.Invoke();
        }
    }

    // Hàm tiện ích để thêm hành động vào hàng đợi
    public void RunOnMainThread(Action action)
    {
        Debug.Log("RunOnMainThread");
        lock (mainThreadActions)
        {
            mainThreadActions.Enqueue(action);
        }
    }
    // Các hàm Handle...
    public void HandelConnection(string urlConnect)
    {
        RunOnMainThread(() =>
        {
            UIManager.instance.FinishConnectionUI(urlConnect);
        });
    }

    public void HandelRegisterTrue()
    {
        RunOnMainThread(() =>
        {
            Player.instance.InitUsername();
            UIManager.instance.uiAuth.UIRegisterSucess();
            UIManager.instance.uiRegisterName.OnRegisterNameUI();
        });
    }
    public void HandelLoginTrue()
    {
        RunOnMainThread(() =>
        {
            Player.instance.InitUsername();
            UIManager.instance.uiAuth.UILoginSuccess();
            
            RequestGetAllDataPlayer();
        });
    }
    public void HandleRegisterFail(string dialog)
    {
        RunOnMainThread(() =>
        {
            UIManager.instance.uiAuth.UIRegisterFailed(dialog);
        });
    }
    public void HandleLoginFail(string dialog)
    {
        RunOnMainThread(() =>
        {
            UIManager.instance.uiAuth.UILoginFailed(dialog);
        });
    }

    public void HandleRegisterNameTrue(string dialog)
    {
        RunOnMainThread(() =>
        {
            Player.instance.InitNamePlayer();
            UIManager.instance.uiRegisterName.SetTextDialogRegisterNameTrue(dialog);
            UIManager.instance.uiRegisterName.CloseUIRegister(3f);
            
            RequestGetAllDataPlayer();
        });
    }
    public void HandleRegisterNameFail(string dialog)
    {
        RunOnMainThread(() =>
        {
            UIManager.instance.uiRegisterName.SetTextDialogRegisterNameFalse(dialog);
        });
    }
    public void HandleGetDataPlayer(PlayerInfo playerInfo)
    {
        RunOnMainThread(() =>
        {
            
            Player.instance.UpdateInformationPlayer(playerInfo.namePlayer, playerInfo.dayPlayer);
            Player.instance.UpdateResourcePlayer(playerInfo.assetData);
        });
    }
    public void HandleGetDataShop(UpdateStoreData data)
    {
        RunOnMainThread(() =>
        {
            if (data != null)
            {
                UIManager.instance.UpdateStoreData(data);
            }
        });
    }

    public void HandleUpdateUIPlayerCanAttack(List<PlayerInfo> playerInfo)
    {
        RunOnMainThread(() =>
        {
            if (playerInfo != null)
            {
                UIManager.instance.uiPK.InitUI(playerInfo);
            }
        });
    }
    public void HandleMessagePlayer(string namePlayer, string message)
    {
        RunOnMainThread(() =>
        {
            if (namePlayer != Player.instance.NamePlayer)
            {
                UIManager.instance.uiChat.CheckAddMessageOpponent(namePlayer, message);
            }
        });
    }

    public void RequestRegister(string username, string password)
    {
        Player.instance.UsernameWaitRegister = username;
        
        AuthData authData = new AuthData(null, username, password);
        RequestPacket requestPacket = new RequestPacket(TypeRequest.REGISTER_NEW_PLAYER, authData);
        ClientManager.Instance.HandelDataAndSend(requestPacket, false);
    }
    public void RequestLogin(string username, string password)
    {
        Player.instance.UsernameWaitRegister = username;
        
        AuthData authData = new AuthData(null, username, password);
        RequestPacket requestPacket = new RequestPacket(TypeRequest.LOGIN_PLAYER,authData);
        ClientManager.Instance.HandelDataAndSend(requestPacket, false);
    }
    public void RequestRegisterName(string namePlayer)
    {
        Player.instance.NamePlayerWaitRegister = namePlayer;
        
        PlayerInfo playerInfo = new PlayerInfo(Player.instance.Username,namePlayer);
        RequestPacket requestPacket = new RequestPacket(TypeRequest.REGISTER_NAME, playerInfo);
        ClientManager.Instance.HandelDataAndSend(requestPacket, false);
    }

    public void RequestGetAllDataPlayer()
    {
        PlayerInfo playerInfo = new PlayerInfo(Player.instance.Username);
        RequestPacket requestPacket = new RequestPacket(TypeRequest.GET_ALL_DATA_PLAYER, playerInfo);
        ClientManager.Instance.HandelDataAndSend(requestPacket, false);
    }
    public void RequestGetDataPlayer()
    {
        PlayerInfo playerInfo = new PlayerInfo(Player.instance.Username);
        
    }
    // Các hàm Request gửi yêu cầu
    public void RequestBuy(ItemType itemType, int count)
    {
        Trade trade = new Trade(Player.instance.Username, TypeObject.EnumToString(itemType), count);
        Debug.Log(trade.username);
        RequestPacket newRequest = new RequestPacket(TypeRequest.BUY, trade);
        ClientManager.Instance.HandelDataAndSend(newRequest, false);
    }
    public void RequestSell(ItemType itemType, int count)
    {
        Trade trade = new Trade(Player.instance.Username, TypeObject.EnumToString(itemType), count);
        RequestPacket newRequest = new RequestPacket(TypeRequest.SELL, trade);
        ClientManager.Instance.HandelDataAndSend(newRequest, false);
    }
    public void RequestUpdateStore()
    {
        RequestPacket1 request = new RequestPacket1(PacketType.UpdateStore);
        ClientManager.Instance.HandelDataAndSend(request, false);
    }
    public void RequestMessage(string namePlayer, string message)
    {
        RequestPacket1 request = new RequestPacket1(PacketType.MessagePlayer, namePlayer, message);
        ClientManager.Instance.HandelDataAndSend(request, false);
    }

    public void RequestFindPlayerCanAttack()
    {
        RequestPacket1 request = new RequestPacket1(PacketType.FindPlayerCanAttack);
        ClientManager.Instance.HandelDataAndSend(request, false);
    }

    public void RequestDayPlay(float dayPlay, SoldierData soldier)
    {
        RequestPacket1 request = new RequestPacket1(PacketType.DayPlay, "" + dayPlay, soldier);
        ClientManager.Instance.HandelDataAndSend(request, false);
    }

    public void RequestAttackPlayer()
    {
        RequestPacket1 request = new RequestPacket1(PacketType.AttackPlayer);
        ClientManager.Instance.HandelDataAndSend(request, false);
    }

    
}
