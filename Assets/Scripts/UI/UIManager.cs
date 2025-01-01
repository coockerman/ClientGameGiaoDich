using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
        public static UIManager instance;
        [SerializeField] List<ScriptableObj> scriptableObjs;
        public UIOpenBuild UiOpenBuild;
        public UIViewItemPrefab UiViewItemPrefab;
        public Transform UIViewItemContent;
        
        public GameObject UIViewListBtn;
        public GameObject UIViewItem;
        public GameObject UIHuongDan;
        
        public bool isInitUIViewItems = false;
        
        public UIViewItemPrefab uiViewMoney;
        public UIInformation uiInformation;
        public UIRegisterName uiRegisterName;
        public UIConnection uiConnection;
        public UIChat uiChat;
        public UIShop uiShop;
        public UIBuilding uiBuilding;
        public UIViewListGround uiViewListGround;
        public UIInfoVP uiInfoVp;
        public UIPK uiPK;
        public UIAuth uiAuth;
        public Builder builder;
        
        public TextMeshProUGUI uiInfoConnectText;

        public Button btnHuongDan;
        public Button btnCloseHuongDan;
        public Button btnOnShop;
        public Button btnOnPK;
        
        private List<UIViewItemPrefab> uiViewItemPrefabs = new List<UIViewItemPrefab>();
        private bool isInitImgShop = false;
        
        private void Awake()
        {
                instance = this;
                
        }

        public void FinishConnectionUI(string urlConnect)
        {
                //UpdateUIViewItems();
                uiConnection.CloseConnectionUI();
                uiAuth.OnAuthUI();
                uiInfoConnectText.text = "Địa chỉ máy chủ: " + urlConnect;
        }
        public void UpdateUIViewItems(AssetData assetData)
        {
                
                if (isInitUIViewItems == false)
                {
                        foreach (ComboItem combo in assetData.assets)
                        {
                                UIViewItemPrefab item = Instantiate(UiViewItemPrefab, UIViewItemContent);
                                ItemType itemType = TypeObject.StringToEnum(combo.Type);
                                item.Init(itemType, GetImageObjByType(itemType).nameObj ,GetImageObjByType(itemType).sprite,combo.Count);
                                uiViewItemPrefabs.Add(item);
                        }
                        uiViewMoney.Init(assetData.countMoney);
                        isInitUIViewItems = true;
                }
                else
                {
                        for (int i = 0; i < uiViewItemPrefabs.Count; i++)
                        {
                                ItemType itemType = TypeObject.StringToEnum(assetData.assets[i].Type);
                                uiViewItemPrefabs[i].Init(itemType, GetImageObjByType(itemType).nameObj,GetImageObjByType(itemType).sprite, assetData.assets[i].Count);
                        }
                        
                        uiViewMoney.Init(assetData.countMoney);
                }
        }
        
        public void OnUIOpenBuild(string order, string title, string status, UnityAction callback)
        {
                UiOpenBuild.gameObject.SetActive(true);
                UiOpenBuild.Init(order, title, status, callback);
        }

        public void HandleUpdateUIRegisterFinish(string namePlayer)
        {
                uiInformation.Init(namePlayer, 1);
                
                OnUIChat();
                
                //uiRegisterName.SetTextDialogRegister("Đăng kí thành công", Color.green);
                uiRegisterName.CloseUIRegister(1.5f);
                uiInformation.OnUIInformation();
                uiViewListGround.OnUIViewListGround();
                builder.OnBuilder();
                OnUIViewItem();
                SoundManager.instance.PlayMusicInGame();
                
                OnUIHuongDan();
                btnHuongDan.onClick.AddListener(OnUIHuongDan);
                btnCloseHuongDan.onClick.AddListener(OffUIHuongDan);
                btnOnShop.onClick.AddListener(OnUIShop);
                btnOnPK.onClick.AddListener(OnUIPK);
                
                OnUIListBtn();
        }
        public void OffUIOpenBuild()
        {
                UiOpenBuild.OffOpenBuild();
        }

        void OnUIViewItem()
        {
                SlideFromTop(UIViewItem);
        }

        void OnUIListBtn()
        {
                SlideFromTop(UIViewListBtn);
        }

        
        
        public ScriptableObj GetImageObjByType(ItemType itemType)
        {
                foreach (ScriptableObj scriptableObj in scriptableObjs)
                {
                        if(scriptableObj.itemType == itemType) return scriptableObj;
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
        void OnUIShop()
        {
                uiShop.OnShop();
        }

        void OnUIPK()
        {
                if (Player.instance.Day >= 10)
                {
                        uiPK.OnPK();
                }
        }
        public void OnUIHuongDan()
        {
                UIHuongDan.gameObject.SetActive(true);
        }
        public void OffUIHuongDan()
        {
                UIHuongDan.gameObject.SetActive(false);
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

        public void SendUpdateStore()
        {
                GameManager.instance.RequestUpdateStore();
        }
}