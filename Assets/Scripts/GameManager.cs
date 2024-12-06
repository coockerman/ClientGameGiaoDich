using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
        public static GameManager instance;

        private void Awake()
        {
                instance = this;
        }

        public void HanderBuy(AbstractData data)
        {
                if (data.isStatus == true)
                {
                        Player.instance.CheckAddAsset(data.itemType, data.price, data.count);
                }
                else
                {
                        Debug.Log("Hết đồ");
                }
        }
        public void HanderSell(AbstractData data)
        {
                if (data.isStatus == true)
                {
                        Player.instance.CheckRemoveAsset(data.itemType, data.price, data.count);
                }
        }

        public void HanderUpdateStore(UpdateStoreData data)
        {
                if (data != null)
                {
                        Debug.Log(data);
                }
        }

        public void RequestGetPlayerCanAttack()
        {
                
        }

        public void HanderResultAttack()
        {
                
        }
}