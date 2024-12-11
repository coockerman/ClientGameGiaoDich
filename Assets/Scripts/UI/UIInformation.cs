using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInformation : MonoBehaviour
{
    public TextMeshProUGUI txtNamePlayer;
    public TextMeshProUGUI txtDayPlayer;

    public void Init(string namePlayer, int dayPlayer)
    {
        txtNamePlayer.text = namePlayer;
        txtDayPlayer.text = "Day: " + dayPlayer.ToString();
    }

    public void UpdateDayPlayer(int dayPlayer)
    {
        txtDayPlayer.text = "Day: " + dayPlayer.ToString();
    }
}
