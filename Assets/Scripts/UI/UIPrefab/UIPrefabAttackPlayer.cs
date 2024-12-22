using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPrefabAttackPlayer : MonoBehaviour
{
    public TextMeshProUGUI txtStt;
    public TextMeshProUGUI txtNamePlayer;
    public TextMeshProUGUI txtDayPlayer;
    public Button btnAttackPlayer;

    public void Init(int stt, string namePlayer, string dayPlayer, UnityAction cbAttack)
    {
        txtStt.text = stt.ToString();
        txtNamePlayer.text = namePlayer;
        txtDayPlayer.text = dayPlayer;
        btnAttackPlayer.onClick.AddListener(cbAttack);
    }
}
