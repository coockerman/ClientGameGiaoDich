using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGround
{
    public string username;
    public int position;
    public string typeBuild;
    public List<ComboItem> comboItemList;

    public BuildGround(){}
    public BuildGround(string username,int position, string typeBuild, List<ComboItem> comboItemList) {
        this.username = username;
        this.position = position;
        this.typeBuild = typeBuild;
        this.comboItemList = comboItemList;
    }
}
