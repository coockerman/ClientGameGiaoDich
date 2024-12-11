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
        public UIViewItemPrefab UiViewItemPrefab;
        public Transform UIViewItem;
        public bool isInitUIViewItems = false;
        public UIViewItemPrefab uiViewMoney;
        public UIInformation uiInformation;
        List<UIViewItemPrefab> uiViewItemPrefabs = new List<UIViewItemPrefab>();
        private void Awake()
        {
                instance = this;
                
        }

        private void Start()
        {
                UpdateUIViewItems();
        }

        public void UpdateUIViewItems()
        {
                if (isInitUIViewItems == false)
                {
                        foreach (ScriptableObj scriptableObj in scriptableObjs)
                        {
                                if (scriptableObj.typeObj == TypeObj.Item)
                                {
                                        UIViewItemPrefab newViewItem = Instantiate(UiViewItemPrefab, UIViewItem);
                                        ItemType itemType = scriptableObj.itemType;
                                        newViewItem.Init(TypeObj.Item, itemType, scriptableObj.sprite, (int) Player.instance.GetResourceAmount(itemType));
                                        uiViewItemPrefabs.Add(newViewItem);
                                }else if (scriptableObj.typeObj == TypeObj.Solier)
                                {
                                        UIViewItemPrefab newViewItem = Instantiate(UiViewItemPrefab, UIViewItem);
                                        SolierType solierType = scriptableObj.solierType;
                                        newViewItem.Init(TypeObj.Solier, solierType, scriptableObj.sprite, (int) Player.instance.GetSolierAmount(solierType));
                                        uiViewItemPrefabs.Add(newViewItem);
                                }
                        }
                        isInitUIViewItems = true;
                }
                else if(isInitUIViewItems)
                {
                        foreach (UIViewItemPrefab uiViewItemPrefab in uiViewItemPrefabs)
                        {
                                if (uiViewItemPrefab.typeObj == TypeObj.Item)
                                {
                                        int count = (int)Player.instance.GetResourceAmount(uiViewItemPrefab.itemType);
                                        uiViewItemPrefab.Init(count);
                                }else if (uiViewItemPrefab.typeObj == TypeObj.Solier)
                                {
                                        int count = (int)Player.instance.GetSolierAmount(uiViewItemPrefab.solierType);
                                        uiViewItemPrefab.Init(count);
                                }
                        }
                }
                uiViewMoney.Init((int) Player.instance.GetMoneyAmount());
        }

        public void OnUIOpenBuild(string order, string title, string status, UnityAction callback)
        {
                UiOpenBuild.gameObject.SetActive(true);
                UiOpenBuild.Init(order, title, status, callback);
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