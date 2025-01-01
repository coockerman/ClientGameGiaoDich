using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public string userName;
    public string ipPlayer;
    public string namePlayer;
    public int dayPlayer;
    public AssetData assetData;
    public BuildData buildData;
    
    public PlayerInfo() {
    }

    public PlayerInfo(string userName)
    {
        this.userName = userName;
    }
    public PlayerInfo(string userName, string namePlayer) {
        this.userName = userName;
        this.namePlayer = namePlayer;
    }

    public PlayerInfo(string namePlayer, int dayPlayer)
    {
        this.namePlayer = namePlayer;
        this.dayPlayer = dayPlayer;
    }

}
