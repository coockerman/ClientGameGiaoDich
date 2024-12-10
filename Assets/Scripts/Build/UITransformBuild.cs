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
                                        UIManager.instance.OnUIOpenBuild(GetItemOpenBuild(), () => { OpenBuild(); });
                                }
                                else
                                {
                                        UIManager.instance.OnUIOpenBuild(GetItemOpenBuild()+ "(Bạn ko đủ nguyên liệu)",
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
        
        void ClearBuild()
        {
                scriptableBuild = null;
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