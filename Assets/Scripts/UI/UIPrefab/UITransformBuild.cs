using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UITransformBuild : MonoBehaviour
{
        public int order;
        public List<ComboItemNeed> listComboItemOpenBuild = new List<ComboItemNeed>();
        public ScriptableBuild scriptableBuild = null;
        public Button btnClickOpenKey;
        public Button btnClickBuilding;
        public Button btnHaveBuild;
        public Button btnDestroyBuild;
        
        public TextMeshProUGUI txtTimeBuild;
        public ParticleSystem handleBuilding;
        public ParticleSystem finishBuild;
        
        private float countTimeBuild = 0;
        
        private void Update()
        {
                if (countTimeBuild > 0 && scriptableBuild == null)
                {
                        countTimeBuild -= Time.deltaTime;
                        txtTimeBuild.text = countTimeBuild.ToString("0.0");
                }
                else
                {
                        txtTimeBuild.text = "";
                }
        }

        
        public void UpdateUIBuild(ComboBuilder comboBuilder)
        {
                if (comboBuilder.statusBuild == TypeStatusGround.NOT_OPEN)
                {
                        btnClickBuilding.gameObject.SetActive(false);
                        btnHaveBuild.gameObject.SetActive(false);
                        
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
                }else if (comboBuilder.statusBuild == TypeStatusGround.OPEN)
                {
                        btnClickOpenKey.gameObject.SetActive(false);
                        btnHaveBuild.gameObject.SetActive(false);

                        btnClickBuilding.gameObject.SetActive(true);
                        btnClickBuilding.onClick.RemoveAllListeners();
                        btnClickBuilding.onClick.AddListener(() =>
                        {
                                UIManager.instance.uiBuilding.OnUIBuilding(this);
                        });
                }else if (comboBuilder.statusBuild == TypeStatusGround.HAVE_BUILD)
                {
                        btnClickBuilding.onClick.RemoveAllListeners();
                        btnClickBuilding.gameObject.SetActive(false);
                        btnClickOpenKey.gameObject.SetActive(false);
                        
                        ScriptableBuild scBuild = getTypeBuild(comboBuilder.typeBuild);
                        
                        this.scriptableBuild = scBuild;
                        
                        btnHaveBuild.gameObject.GetComponent<Image>().sprite = this.scriptableBuild.buildSprite;
                        btnHaveBuild.onClick.AddListener(ChangeStatusDestroy);
                        btnHaveBuild.gameObject.SetActive(true);
                        
                        btnDestroyBuild.onClick.AddListener(ClearBuild);
                        
                }
        }
        
        ScriptableBuild getTypeBuild(string typeBuild)
        {
                ItemType itemType = TypeObject.StringToEnum(typeBuild);
                foreach (ScriptableBuild scBuild in UIManager.instance.scriptableBuilds)
                {
                        if(scBuild.itemType == itemType) return scBuild;
                }
                Debug.Log("Ko tìm thấy");
                return null;
        }
        void ChangeStatusDestroy()
        {
                btnDestroyBuild.gameObject.SetActive(!btnDestroyBuild.isActiveAndEnabled);
        }
        public void Building(ScriptableBuild scriptableBuild)
        {
                StartCoroutine(IEBuilding(scriptableBuild));
        }

        IEnumerator IEBuilding(ScriptableBuild scriptableBuild)
        {
                btnClickBuilding.onClick.RemoveAllListeners();
                handleBuilding.gameObject.SetActive(true);
                countTimeBuild = scriptableBuild.timeBuild;
                
                SoundManager.instance.PlayBuidingSound();
                
                yield return new WaitForSeconds(scriptableBuild.timeBuild);
                
                handleBuilding.gameObject.SetActive(false);
                finishBuild.gameObject.SetActive(true);
                btnClickBuilding.gameObject.SetActive(false);
                
                this.scriptableBuild = scriptableBuild;
                btnHaveBuild.gameObject.GetComponent<Image>().sprite = this.scriptableBuild.buildSprite;
                btnHaveBuild.onClick.AddListener(ChangeStatusDestroy);
                btnHaveBuild.gameObject.SetActive(true);
                
                SoundManager.instance.PlayFinishBuildSound();
                
                btnDestroyBuild.onClick.AddListener(ClearBuild);
                yield return new WaitForSeconds(1f);
                finishBuild.gameObject.SetActive(false);
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
                UIManager.instance.OffUIOpenBuild();
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