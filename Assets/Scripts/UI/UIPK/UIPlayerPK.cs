using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerPK : MonoBehaviour
{
    public TextMeshProUGUI namePlayer, txtCungThu, txtChienBinh, txtKyBinh;

    public void InitUI(InfoPlayer infoPlayer)
    {
        Clean();
        namePlayer.text = infoPlayer.namePlayer;
        txtCungThu.text = "Cung thủ: " + infoPlayer.soldierData.getArrow().ToString();
        txtChienBinh.text = "Chiến binh: " + infoPlayer.soldierData.getMelee().ToString();
        txtKyBinh.text = "Kỵ binh: " + infoPlayer.soldierData.getCavalry().ToString();
    }

    void Clean()
    {
        namePlayer.text = "";
        txtCungThu.text = "";
        txtChienBinh.text = "";
        txtKyBinh.text = "";
    }
}
