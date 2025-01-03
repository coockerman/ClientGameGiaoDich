using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGround
{
    public string username;
    public int position;
    public string typeBuild;
    public int reward;
    public List<ComboItem> comboItemList;

    public BuildGround(){}
    public BuildGround(string username,int position, string typeBuild, int reward, List<ComboItem> comboItemList) {
        this.username = username;
        this.position = position;
        this.typeBuild = typeBuild;
        this.reward = reward;
        this.comboItemList = comboItemList;
    }
}
