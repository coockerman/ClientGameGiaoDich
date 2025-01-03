using System;
using System.Collections.Generic;
using System.Threading;
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
            UIManager.instance.HandleUpdateUIRegisterFinish(false);
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
    public void HandleGetPlayerCanAttack(List<PlayerInfo> listPlayerInfo)
    {
        RunOnMainThread(() =>
        {
            UIManager.instance.uiPK.InitUI(listPlayerInfo);
        });
    }
    public void HandleRegisterNameTrue(string dialog)
    {
        RunOnMainThread(() =>
        {
            Player.instance.InitNamePlayer();
            UIManager.instance.uiRegisterName.SetTextDialogRegisterNameTrue(dialog);
            UIManager.instance.uiRegisterName.CloseUIRegister(3f);
            
            UIManager.instance.HandleUpdateUIRegisterFinish(true);
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
    public void HandleResetPasswordTrue(string dialog)
    {
        RunOnMainThread(() =>
        {
            UIManager.instance.uiUpdatePassword.UpdatePassTrue(dialog);
        });
    }
    public void HandleResetPasswordFalse(string dialog)
    {
        RunOnMainThread(() =>
        {
            UIManager.instance.uiUpdatePassword.UpdatePassFalse(dialog);
        });
    }

    public void HandleLogoutTrue()
    {
        RunOnMainThread(() =>
        {
            Player.instance.LogoutPlayer();
            UIManager.instance.UpdateUILogout();
        });
    }
    public void HandleGetDataPlayer(PlayerInfo playerInfo)
    {
        RunOnMainThread(() =>
        {
            Player.instance.UpdateInformationPlayer(playerInfo.namePlayer, playerInfo.dayPlayer);
            Player.instance.UpdateResourcePlayer(playerInfo.assetData);
            Player.instance.UpdateBuildPlayer(playerInfo.buildData);
        });
    }
    public void HandleUpday()
    {
        RunOnMainThread(() =>
        {
            RequestGetAllDataPlayer();
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

    
    public void HandleMessagePlayer(ChatMessage chatMessage)
    {
        RunOnMainThread(() =>
        {
            if (chatMessage.namePlayer != Player.instance.NamePlayer)
            {
                UIManager.instance.uiChat.CheckAddMessageOpponent(chatMessage.namePlayer, chatMessage.message);
            }
        });
    }
    
    
    
    // Các hàm Request gửi yêu cầu
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
    public void RequestLogout()
    {
        AuthData authData = new AuthData(null, Player.instance.Username, "");
        RequestPacket newRequest = new RequestPacket(TypeRequest.LOGOUT_PLAYER, authData);
        ClientManager.Instance.HandelDataAndSend(newRequest, false);
    }

    public void RequestForgetPassword(string username)
    {
        AuthData authData = new AuthData(null, username, "");
        RequestPacket newRequest = new RequestPacket(TypeRequest.FORGET_PASSWORD, authData);
        ClientManager.Instance.HandelDataAndSend(newRequest, false);
    }

    public void RequestResetPassword(string passwordOld, string passwordNew)
    {
        PasswordReset passwordReset = new PasswordReset(Player.instance.Username, passwordOld, passwordNew);
        RequestPacket newRequest = new RequestPacket(TypeRequest.PASSWORD_RESET, passwordReset);
        ClientManager.Instance.HandelDataAndSend(newRequest, false);
    }
    public void RequestGetAllDataPlayer()
    {
        PlayerInfo playerInfo = new PlayerInfo(Player.instance.Username);
        RequestPacket requestPacket = new RequestPacket(TypeRequest.GET_ALL_DATA_PLAYER, playerInfo);
        ClientManager.Instance.HandelDataAndSend(requestPacket, false);
    }
    
    
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

    public void RequestOpenBuild(List<ComboItem> combos, int stt, int reward)
    {
        BuildGround buildGround = new BuildGround(Player.instance.Username, stt, "",reward, combos);
        RequestPacket newRequest = new RequestPacket(TypeRequest.OPEN_BUILD, buildGround);
        ClientManager.Instance.HandelDataAndSend(newRequest, false);
    }
    public void RequestBuilding(List<ComboItem> combos, string typeBuild ,int stt, int reward)
    {
        BuildGround buildGround = new BuildGround(Player.instance.Username, stt, typeBuild,reward, combos);
        RequestPacket newRequest = new RequestPacket(TypeRequest.BUILDING, buildGround);
        ClientManager.Instance.HandelDataAndSend(newRequest, false);
    }

    public void RequestCleanBuild(int stt)
    {
        BuildGround buildGround = new BuildGround();
        buildGround.username = Player.instance.Username;
        buildGround.position = stt;
        RequestPacket newRequest = new RequestPacket(TypeRequest.CLEAN_BUILD, buildGround);
        ClientManager.Instance.HandelDataAndSend(newRequest, false);
    }

    public void RequestChatMessageAll(string message)
    {
        ChatMessage chatMessage = new ChatMessage();
        chatMessage.namePlayer = Player.instance.NamePlayer;
        chatMessage.message = message;
        RequestPacket newRequest = new RequestPacket(TypeRequest.MESSAGE, chatMessage);
        ClientManager.Instance.HandelDataAndSend(newRequest, false);
    }

    public void RequestGetPlayerCanAttack()
    {
        RequestPacket newRequest = new RequestPacket(TypeRequest.GET_PLAYER_ATTACK);
        ClientManager.Instance.HandelDataAndSend(newRequest, false);
    }
    
    
}
