using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerPK : MonoBehaviour
{
    public TextMeshProUGUI namePlayer, txtCungThu, txtChienBinh, txtKyBinh;

    public void InitUI(PlayerInfo playerInfo)
    {
        Clean();
        namePlayer.text = playerInfo.namePlayer;
        txtCungThu.text = "Cung thủ: " + playerInfo.assetData.GetAssetCountByType(TypeObject.ARROW);
        txtChienBinh.text = "Chiến binh: " + playerInfo.assetData.GetAssetCountByType(TypeObject.MELEE);
        txtKyBinh.text = "Kỵ binh: " + playerInfo.assetData.GetAssetCountByType(TypeObject.CAVALRY);
    }

    void Clean()
    {
        namePlayer.text = "";
        txtCungThu.text = "";
        txtChienBinh.text = "";
        txtKyBinh.text = "";
    }
}
