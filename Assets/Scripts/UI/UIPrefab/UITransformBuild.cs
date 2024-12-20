using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UITransformBuild : MonoBehaviour
{
        public int order;
        public bool isOpen = false;
        public List<ComboItemNeed> listComboItemOpenBuild = new List<ComboItemNeed>();
        public ScriptableBuild scriptableBuild = null;
        public Button btnClickOpenKey;
        public Button btnClickBuilding;
        public Button btnHaveBuild;
        public Button btnDestroyBuild;
        private void Start()
        {
                UpdateUIBuild();
                
        }

        void UpdateUIBuild()
        {
                if (isOpen)
                {
                        btnClickOpenKey.gameObject.SetActive(false);

                        btnClickBuilding.gameObject.SetActive(true);
                        btnClickBuilding.onClick.RemoveAllListeners();
                        btnClickBuilding.onClick.AddListener(() =>
                        {
                                UIManager.instance.uiBuilding.OnUIBuilding(this);
                        });
                }
                else
                {
                        btnClickBuilding.gameObject.SetActive(false);
                        
                        btnClickOpenKey.gameObject.SetActive(true);
                        btnClickOpenKey.onClick.RemoveAllListeners();
                        btnClickOpenKey.onClick.AddListener(() =>
                        {
                                if (CheckOpenCanBuild())
                                {
                                        UIManager.instance.OnUIOpenBuild("Khu đất thứ: " + order.ToString(), 
                                                GetItemOpenBuild(), 
                                                "Bạn có thể mở ô đất",
                                                () => { OpenBuild(); });
                                }
                                else
                                {
                                        UIManager.instance.OnUIOpenBuild("Khu đất thứ: " + order.ToString(),
                                                GetItemOpenBuild(),
                                                "Thiếu nguyên liệu rồi",
                                                () =>
                                                {
                                                        Debug.Log("Ko đủ nguyên liệu");
                                                });
                                }
                        });
                }
        }
        public void GetProduct()
        {
                if (isOpen && scriptableBuild != null)
                {
                        
                }
        }

        void ChangeStatusDestroy()
        {
                btnDestroyBuild.gameObject.SetActive(!btnDestroyBuild.isActiveAndEnabled);
        }
        public void Building(ScriptableBuild scriptableBuild)
        {
                this.scriptableBuild = scriptableBuild;
                btnHaveBuild.gameObject.GetComponent<Image>().sprite = this.scriptableBuild.buildSprite;
                btnHaveBuild.onClick.AddListener(ChangeStatusDestroy);
                btnHaveBuild.gameObject.SetActive(true);
                
                btnClickBuilding.onClick.RemoveAllListeners();
                btnClickBuilding.gameObject.SetActive(false);
                
                btnDestroyBuild.onClick.AddListener(ClearBuild);
        }
        
        void ClearBuild()
        {
                scriptableBuild = null;
                btnHaveBuild.gameObject.GetComponent<Image>().sprite = null;
                btnHaveBuild.onClick.RemoveAllListeners();
                btnHaveBuild.gameObject.SetActive(false);
                
                btnClickBuilding.onClick.AddListener(() =>
                {
                        UIManager.instance.uiBuilding.OnUIBuilding(this);
                });
                btnClickBuilding.gameObject.SetActive(true);
                
                btnDestroyBuild.onClick.RemoveAllListeners();
                btnDestroyBuild.gameObject.SetActive(false);
        }

        void OpenBuild()
        {
                foreach (ComboItemNeed comboItemNeed in listComboItemOpenBuild)
                {
                        Player.instance.CheckRemoveAsset(comboItemNeed.ItemType, 0, comboItemNeed.Count);
                }
                isOpen = true;
                UIManager.instance.OffUIOpenBuild();
                UpdateUIBuild();
        }
        bool CheckOpenCanBuild()
        {
                if (listComboItemOpenBuild.Count > 0)
                {
                        foreach (ComboItemNeed comboItemNeed in listComboItemOpenBuild)
                        {
                                if (comboItemNeed.Count > Player.instance.GetResourceAmount(comboItemNeed.ItemType))
                                {
                                        return false;
                                }
                        }
                        return true;
                }
                return false;
        }

        string GetItemOpenBuild()
        {
                string txt = "";
                if (listComboItemOpenBuild.Count > 0)
                {
                        foreach (ComboItemNeed comboItemNeed in listComboItemOpenBuild)
                        {
                                txt += comboItemNeed.ItemType.ToString() + ": " + comboItemNeed.Count + " | ";
                        }
                }
                return txt;
        }
}