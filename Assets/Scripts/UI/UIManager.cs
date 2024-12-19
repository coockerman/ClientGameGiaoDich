using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
        public static UIManager instance;
        [SerializeField] List<ScriptableObj> scriptableObjs;
        public UIOpenBuild UiOpenBuild;
        public UIViewItemPrefab UiViewItemPrefab;
        public Transform UIViewItemContent;
        
        public GameObject UIViewListBtn;
        public GameObject UIViewItem;
        
        public bool isInitUIViewItems = false;
        
        public UIViewItemPrefab uiViewMoney;
        public UIInformation uiInformation;
        public UIRegisterName uiRegisterName;
        public UIConnection uiConnection;
        public UIChat uiChat;
        public UIShop uiShop;
        public UIBuilding uiBuilding;

        public TextMeshProUGUI uiInfoConnectText;
        
        private List<UIViewItemPrefab> uiViewItemPrefabs = new List<UIViewItemPrefab>();
        private bool isInitImgShop = false;
        
        private void Awake()
        {
                instance = this;
                
        }

        public void FinishConnectionUI(string urlConnect)
        {
                UpdateUIViewItems();
                uiConnection.CloseConnectionUI();
                uiRegisterName.OnRegisterNameUI();
                uiInfoConnectText.text = "Địa chỉ máy chủ: " + urlConnect;
        }
        public void UpdateUIViewItems()
        {
                if (isInitUIViewItems == false)
                {
                        foreach (ScriptableObj scriptableObj in scriptableObjs)
                        {
                                if (scriptableObj.typeObj == TypeObj.Item)
                                {
                                        UIViewItemPrefab newViewItem = Instantiate(UiViewItemPrefab, UIViewItemContent);
                                        ItemType itemType = scriptableObj.itemType;
                                        newViewItem.Init(TypeObj.Item, itemType, scriptableObj.sprite, (int) Player.instance.GetResourceAmount(itemType));
                                        uiViewItemPrefabs.Add(newViewItem);
                                }else if (scriptableObj.typeObj == TypeObj.Solier)
                                {
                                        UIViewItemPrefab newViewItem = Instantiate(UiViewItemPrefab, UIViewItemContent);
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

        public void HandleUpdateUIRegisterFinish(string namePlayer)
        {
                UIManager.instance.uiInformation.Init(namePlayer, 1);
                UIManager.instance.OnUIChat();
                uiRegisterName.SetTextDialogRegister("Đăng kí thành công", Color.green);
                uiRegisterName.CloseUIRegister(1.5f);
                uiInformation.OnUIInformation();
                OnUIViewItem();
                OnUIListBtn();
        }
        public void OffUIOpenBuild()
        {
                UiOpenBuild.OffOpenBuild();
        }

        public void OnUIViewItem()
        {
                SlideFromTop(UIViewItem);
        }

        public void OnUIListBtn()
        {
                SlideFromTop(UIViewListBtn);
        }

        public ScriptableObj GetImageObjByType(TypeObj typeObj, ItemType itemType, SolierType solierType)
        {
                if (typeObj == TypeObj.Solier)
                {
                        foreach (ScriptableObj scriptableObj in scriptableObjs)
                        {
                                if (scriptableObj.typeObj == TypeObj.Solier)
                                {
                                        if(scriptableObj.solierType == solierType) return scriptableObj;
                                }
                        }
                }else if (typeObj == TypeObj.Item)
                {
                        foreach (ScriptableObj scriptableObj in scriptableObjs)
                        {
                                if (scriptableObj.typeObj == TypeObj.Item)
                                {
                                        if(scriptableObj.itemType == itemType) return scriptableObj;
                                }
                        }
                }

                return null;
        }
        public ScriptableObj GetImageObjByType(ItemType itemType)
        {
                foreach (ScriptableObj scriptableObj in scriptableObjs)
                {
                        if (scriptableObj.typeObj == TypeObj.Item)
                        {
                                if(scriptableObj.itemType == itemType) return scriptableObj;
                        }
                }
                return null;
        }
        public ScriptableObj GetImageObjByType(SolierType solierType)
        {
                foreach (ScriptableObj scriptableObj in scriptableObjs)
                {
                        if (scriptableObj.typeObj == TypeObj.Solier)
                        {
                                if(scriptableObj.solierType == solierType) return scriptableObj;
                        }
                }
                return null;
        }
        // Hàm xử lý hiệu ứng trượt từ trên xuống
        private void SlideFromTop(GameObject uiElement)
        {
                RectTransform rectTransform = uiElement.GetComponent<RectTransform>();
                if (rectTransform == null)
                {
                        Debug.LogError("SlideFromTop: Đối tượng không có RectTransform.");
                        return;
                }

                // Lưu vị trí hiện tại (đích đến)
                Vector2 targetPosition = rectTransform.anchoredPosition;

                // Đặt vị trí ban đầu (trên màn hình)
                Vector2 offScreenPosition = new Vector2(targetPosition.x, rectTransform.rect.height + 100); // 100 là khoảng cách tùy chỉnh
                rectTransform.anchoredPosition = offScreenPosition;

                // Bật đối tượng
                uiElement.SetActive(true);

                // Tạo hiệu ứng trượt từ trên xuống
                rectTransform.DOAnchorPos(targetPosition, 0.5f).SetEase(Ease.OutBounce); // Lướt xuống với hiệu ứng nẩy nhẹ
        }
        public void OnUIShop()
        {
                uiShop.OnShop();
        }

        public void OnUIChat()
        {
                uiChat.OnChat();
        }
        
        public void UpdateStoreData(UpdateStoreData data)
        {
                if (!isInitImgShop)
                {
                        isInitImgShop = true;
                        uiShop.UpdateStoreData(data, scriptableObjs);
                }
                else 
                        uiShop.UpdateStoreData(data);
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