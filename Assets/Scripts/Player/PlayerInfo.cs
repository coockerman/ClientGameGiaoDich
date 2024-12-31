using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public string userName;
    
    public string namePlayer;
    
    public PlayerInfo playerInfo;

    public PlayerInfo() {
    }
    public PlayerInfo(string userName, string namePlayer) {
        this.userName = userName;
        this.namePlayer = namePlayer;
    }

    public PlayerInfo(string userName, PlayerInfo playerInfo)
    {
        this.userName = userName;
        this.playerInfo = playerInfo;
    }
}
