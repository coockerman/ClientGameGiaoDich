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

    public void HandelLoginTrue()
    {
        RunOnMainThread(() =>
        {
            UIManager.instance.uiAuth.UILoginSuccess();
        });
    }
    public void HandelRegisterFail(string dialog)
    {
        RunOnMainThread(() =>
        {
            UIManager.instance.uiAuth.UIRegisterFailed(dialog);
        });
    }
    public void HandelLoginFail(string dialog)
    {
        RunOnMainThread(() =>
        {
            UIManager.instance.uiAuth.UILoginFailed(dialog);
        });
    }
    public void HandleBuy(AbstractData data)
    {
        RunOnMainThread(() =>
        {
            if (data.isStatus)
            {
                Player.instance.CheckAddAsset(data.itemType, data.price, data.count);
            }
            else
            {
                Debug.Log("Hết đồ");
            }
        });
    }

    public void HandleSell(AbstractData data)
    {
        RunOnMainThread(() =>
        {
            if (data.isStatus)
            {
                Player.instance.CheckRemoveAsset(data.itemType, data.price, data.count);
            }
        });
    }

    public void HandleUpdateStore(UpdateStoreData data)
    {
        RunOnMainThread(() =>
        {
            if (data != null)
            {
                UIManager.instance.UpdateStoreData(data);
            }
        });
    }

    public void HandleGetNamePlayer(string namePlayer)
    {
        RunOnMainThread(() =>
        {
            if (!string.IsNullOrEmpty(namePlayer))
            {
                Player.instance.SetupNamePlayer(namePlayer);
                
            }
            else
            {
                Debug.Log("Can't get name player");
            }
        });
    }

    public void HandleUpdateUIPlayerCanAttack(List<InfoPlayer> listInfo)
    {
        RunOnMainThread(() =>
        {
            if (listInfo != null)
            {
                UIManager.instance.uiPK.InitUI(listInfo);
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

    public void HandleRegisterPlayer(string namePlayer, bool isRegister)
    {
        RunOnMainThread(() =>
        {
            if (isRegister)
            {
                Player.instance.SetupNamePlayer(namePlayer);
                UIManager.instance.HandleUpdateUIRegisterFinish(namePlayer);
            }
            else
            {
                UIManager.instance.uiRegisterName.SetTextDialogRegister("Tên người chơi đã tồn tại", Color.red);
            }
        });
    }

    public void HandleFailConnection()
    {
        //Load lại game
        SceneManager.LoadScene(0);
    }

    public void HandleFailDeserializeJson()
    {
        
    }
    public void HandleFailGetMessage()
    {
        
    }

    public void RequestRegister(string username, string password)
    {
        AuthData authData = new AuthData(null, username, password);
        RequestPacket requestPacket = new RequestPacket(TypeRequest.REGISTER_NEW_PLAYER, authData);
        ClientManager.Instance.HandelDataAndSend(requestPacket, false);
    }
    public void RequestLogin(string username, string password)
    {
        AuthData authData = new AuthData(null, username, password);
        RequestPacket requestPacket = new RequestPacket(TypeRequest.LOGIN_PLAYER,authData);
        ClientManager.Instance.HandelDataAndSend(requestPacket, false);
    }
    public void RequestRegisterName(string namePlayer)
    {
        //Todo fix
        PlayerInfo playerInfo = new PlayerInfo("nghai1234",namePlayer);
        RequestPacket requestPacket = new RequestPacket(TypeRequest.REGISTER_NAME, playerInfo);
        ClientManager.Instance.HandelDataAndSend(requestPacket, false);
    }
    // Các hàm Request gửi yêu cầu
    public void RequestBuy(bool status, ItemType itemType, int price, int count)
    {
        AbstractData newBuy = new AbstractData(status, itemType, count, price);
        RequestPacket1 newRequest = new RequestPacket1(PacketType.Buy, newBuy);
        ClientManager.Instance.HandelDataAndSend(newRequest, true);
    }
    public void RequestSell(bool status, ItemType itemType, int price, int count)
    {
        AbstractData newSell = new AbstractData(status, itemType, count, price);
        RequestPacket1 newRequest = new RequestPacket1(PacketType.Sell, newSell);
        ClientManager.Instance.HandelDataAndSend(newRequest, true);
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
