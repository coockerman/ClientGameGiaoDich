using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
        public static UIManager instance;
        public List<ScriptableObj> scriptableObjs;
        public UIOpenBuild UiOpenBuild;
        private void Awake()
        {
                instance = this;
        }

        public void UpdateUIResource()
        {
                foreach (ScriptableObj scriptableObj in scriptableObjs)
                {
                        
                }
        }

        public void OnUIOpenBuild(string title, UnityAction callback)
        {
                UiOpenBuild.gameObject.SetActive(true);
                UiOpenBuild.Init(title, callback);
        }

        public void OffUIOpenBuild()
        {
                UiOpenBuild.OffOpenBuild();
        }
        public void SendBuyGold()
        {
                GameManager.instance.RequestBuy(true, ItemType.Gold, 20, 10);
        }
        public void SendSellGold()
        {
                GameManager.instance.RequestSell(true, ItemType.Gold, 20, 10);
        }

        public void SendUpdateStore()
        {
                GameManager.instance.RequestUpdateStore();
        }
}