using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPlayer
{
    public string ipPlayer;
    public string namePlayer;
    public string dayPlayer;
    public SoldierData soldierData;
    
    public InfoPlayer() {}

    public InfoPlayer(string ipPlayer, string namePlayer, string dayPlayer, SoldierData soldierData)
    {
        this.ipPlayer = ipPlayer;
        this.namePlayer = namePlayer;
        this.dayPlayer = dayPlayer;
        this.soldierData = soldierData;
    }
}
