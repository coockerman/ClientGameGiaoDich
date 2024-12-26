using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPK : MonoBehaviour
{
    public GameObject objFindPlayer;
    public GameObject objViewPK;
    public GameObject objViewCombat;
    public GameObject objResult;
    
    public Button btnClose;
    public Button btnUpdateListPlayerCanAttack;

    public UIPrefabAttackPlayer prefabAttackPlayer;
    public Transform contentAttackPlayer;
    
    public UIPlayerPK uiPlayerPKplayer1;
    public UIPlayerPK uiPlayerPKplayer2;

    public ParticleSystem player1PK;
    public ParticleSystem player2PK;
    
    public ParticleSystem player1Win;
    public ParticleSystem player2Win;

    public TextMeshProUGUI txtResult;
    
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
                newPlayer.Init(count, infoPlayer.namePlayer, infoPlayer.dayPlayer, () =>
                {
                    StartCoroutine(IEAttack(infoPlayer));
                });
                listAttackPlayer.Add(newPlayer);
            }
        }
    }

    

    IEnumerator IEAttack(InfoPlayer infoPlayer2)
    {
        objFindPlayer.SetActive(false);
        objViewPK.SetActive(true);

        SoldierData soldierDataPlayer1 = new SoldierData(
            Player.instance.GetSolierAmount(SolierType.Melee),
            Player.instance.GetSolierAmount(SolierType.Arrow),
            Player.instance.GetSolierAmount(SolierType.Cavalry)
        );
        InfoPlayer infoPlayer1 = new InfoPlayer(
            null,
            Player.instance.NamePlayer,
            Player.instance.Day.ToString(),
            soldierDataPlayer1
        );
        
        uiPlayerPKplayer1.InitUI(infoPlayer1);
        uiPlayerPKplayer2.InitUI(infoPlayer2);

        float result = TinhToan(infoPlayer1.soldierData, infoPlayer2.soldierData);
        
        if (result > 0)
        {
            float amountResult = 50 + 5 * Player.instance.Day;
            Player.instance.AddMoneyAmount(amountResult);
            txtResult.text = "Bạn đã chiến thắng " + infoPlayer2.namePlayer + " nhận được " + amountResult + " xu";
        } else if (result < 0)
        {
            txtResult.text = "Bạn đã thua " + infoPlayer2.namePlayer;
        }
        else
        {
            txtResult.text = "Trận đấu hoà";
        }
        yield return new WaitForSeconds(0.1f);
        objViewCombat.SetActive(true);
        
        yield return new WaitForSeconds(1f);
        player1PK.gameObject.SetActive(true);
        player2PK.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(4f);
        if (result > 0)
        {
            player2PK.gameObject.SetActive(false);
            player2Win.gameObject.SetActive(true);
        }else if (result < 0)
        {
            player1PK.gameObject.SetActive(false);
            player1Win.gameObject.SetActive(true);
        }
        
        yield return new WaitForSeconds(1f);
        player1PK.gameObject.SetActive(false);
        player2PK.gameObject.SetActive(false);
        
        player1Win.gameObject.SetActive(false);
        player2Win.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(0.8f);
        
        objViewPK.SetActive(false);
        objResult.SetActive(true);
        yield return new WaitForSeconds(4f);
        objResult.SetActive(false);
        ClosePK();
    }
    float TinhToan(SoldierData soldierDataPlayer1, SoldierData soldierDataPlayer2)
    {
        float lcMelee = 2f;
        float lcArrow = 5f;
        float lcCavalry = 25f;
        float lc1 = lcMelee*soldierDataPlayer1.getMelee() + lcArrow*soldierDataPlayer1.getArrow() + lcCavalry*soldierDataPlayer1.getCavalry();
        float lc2 = lcMelee*soldierDataPlayer2.getMelee() + lcArrow*soldierDataPlayer2.getArrow() + lcCavalry*soldierDataPlayer2.getCavalry();
        if (lc1 > lc2) return 1;
        else if (lc1 < lc2) return -1;
        else return 0;

    }
    void UpdatePlayerCanAttack()
    {
        GameManager.instance.RequestFindPlayerCanAttack();
    }
    
    public void OnPK()
    {
        CleanUIPK();
        objFindPlayer.SetActive(true);
        objViewPK.SetActive(false);
        objResult.SetActive(false);
        
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