using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPK : MonoBehaviour
{
    public GameObject objFindPlayer;
    public GameObject objViewPK;
    
    public Button btnClose;
    public Button btnUpdateListPlayerCanAttack;

    public UIPrefabAttackPlayer prefabAttackPlayer;
    public Transform contentAttackPlayer;
    private List<UIPrefabAttackPlayer> listAttackPlayer = new List<UIPrefabAttackPlayer>();

    public void InitUI(List<InfoPlayer> listInfo)
    {
        CleanListPlayer();
        if (listInfo.Count > 0)
        {
            int count = 0;
            foreach (InfoPlayer infoPlayer in listInfo)
            {
                count++;
                UIPrefabAttackPlayer newPlayer = Instantiate(prefabAttackPlayer, contentAttackPlayer);
                newPlayer.Init(count, infoPlayer.namePlayer, infoPlayer.dayPlayer, Attack);
                listAttackPlayer.Add(newPlayer);
            }
        }
    }

    void Attack()
    {
        //Todo attack
    }
    void UpdatePlayerCanAttack()
    {
        GameManager.instance.RequestFindPlayerCanAttack();
    }
    
    public void OnPK()
    {
        CleanUIPK();
        objViewPK.SetActive(true);
        objFindPlayer.SetActive(false);
        
        gameObject.SetActive(true);
        btnClose.onClick.AddListener(ClosePK);
        btnUpdateListPlayerCanAttack.onClick.AddListener(UpdatePlayerCanAttack);
        UpdatePlayerCanAttack();
    }

    void ClosePK()
    {
        CleanUIPK();
        gameObject.SetActive(false);
    }

    void CleanUIPK()
    {
        btnClose.onClick.RemoveAllListeners();
        btnUpdateListPlayerCanAttack.onClick.RemoveAllListeners();
    }

    void CleanListPlayer()
    {
        if (listAttackPlayer.Count <= 0) return;
        foreach (UIPrefabAttackPlayer player in listAttackPlayer)
        {
            Destroy(player.gameObject);
        }
        listAttackPlayer.Clear();
    }
}
