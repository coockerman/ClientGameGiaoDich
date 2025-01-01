using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
    
    [FormerlySerializedAs("player1Win")] public ParticleSystem player1Die;
    [FormerlySerializedAs("player2Win")] public ParticleSystem player2Die;

    public TextMeshProUGUI txtResult;
    
    private List<UIPrefabAttackPlayer> listAttackPlayer = new List<UIPrefabAttackPlayer>();

    public void InitUI(List<PlayerInfo> listInfo)
    {
        CleanListPlayer();
        if (listInfo.Count > 0)
        {
            int count = 0;
            foreach (PlayerInfo playerInfo in listInfo)
            {
                if (playerInfo.namePlayer != Player.instance.NamePlayer)
                {
                    count++;
                
                    UIPrefabAttackPlayer newPlayer = Instantiate(prefabAttackPlayer, contentAttackPlayer);
                    newPlayer.Init(count, playerInfo.namePlayer, playerInfo.dayPlayer.ToString(), () =>
                    {
                        StartCoroutine(IEAttack(playerInfo));
                    });
                    listAttackPlayer.Add(newPlayer);
                }
            }
        }
    }

    IEnumerator IEAttack(PlayerInfo playerInfo)
    {
        objFindPlayer.SetActive(false);
        objViewPK.SetActive(true);

        PlayerInfo playerInfoSelf = new PlayerInfo(
            Player.instance.NamePlayer,
            Player.instance.Day
        );
        
        uiPlayerPKplayer1.InitUI(playerInfoSelf);
        uiPlayerPKplayer2.InitUI(playerInfo);

        float result = 1;
        
        if (result > 0)
        {
            float amountResult = 50 + 5 * Player.instance.Day;
            Player.instance.AddMoneyAmount(amountResult);
            txtResult.text = "Bạn đã chiến thắng " + playerInfo.namePlayer + " nhận được " + amountResult + " xu";
        } else if (result < 0)
        {
            txtResult.text = "Bạn đã thua " + playerInfo.namePlayer;
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
        
        SoundManager.instance.PlayPKSound();
        
        yield return new WaitForSeconds(4f);
        if (result > 0)
        {
            player2PK.gameObject.SetActive(false);
            player2Die.gameObject.SetActive(true);
        }else if (result < 0)
        {
            player1PK.gameObject.SetActive(false);
            player1Die.gameObject.SetActive(true);
        }
        
        SoundManager.instance.PlayChiMangSound();
        
        yield return new WaitForSeconds(1f);
        player1PK.gameObject.SetActive(false);
        player2PK.gameObject.SetActive(false);
        
        player1Die.gameObject.SetActive(false);
        player2Die.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(0.8f);
        
        objViewPK.SetActive(false);
        objResult.SetActive(true);
        
        if(result > 0) SoundManager.instance.PlayWinSound();
        else if(result < 0) SoundManager.instance.PlayLoseSound();
        
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
