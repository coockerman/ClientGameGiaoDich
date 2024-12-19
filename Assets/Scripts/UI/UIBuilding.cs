using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBuilding : MonoBehaviour
{
    public Transform boxBtnBuilding;
    public Button prefabBtnBuilding;
    public List<ScriptableBuild> listBuild = new List<ScriptableBuild>();
    bool isInit = false;
    List<Button> buttonList = new List<Button>();
    
    public Image imgBuild;
    public TextMeshProUGUI description;
    public TextMeshProUGUI nameBuild;
    public TextMeshProUGUI countProduct;
    public TextMeshProUGUI typeBuild;
    public TextMeshProUGUI dialogErrorBuild;
    
    public Transform boxComboItemNeed;
    public GameObject prefabComboItemNeed;
    List<GameObject> comboItems = new List<GameObject>();

    public Button building;
    public Button closeBtn;

    void InitListBuild(UITransformBuild transformBuild)
    {
        CleanUIListView();
        SetupUIViewBuildingInfo(listBuild[0], transformBuild);
        if (isInit == false)
        {
            foreach (ScriptableBuild build in listBuild)
            {
                ScriptableBuild localBuild = build;
                Button newListBtn = Instantiate(prefabBtnBuilding, boxBtnBuilding);
                newListBtn.GetComponent<Image>().sprite = build.btnSprite;
                newListBtn.onClick.AddListener(() =>
                {
                    SetupUIViewBuildingInfo(localBuild, transformBuild);
                });
                buttonList.Add(newListBtn);
            }
            isInit = true;
        }else if (isInit && buttonList.Count > 0)
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                int localIndex = i;
                buttonList[localIndex].onClick.AddListener(() =>
                {
                    if (listBuild[localIndex] != null)
                    {
                        SetupUIViewBuildingInfo(listBuild[localIndex], transformBuild);
                    }
                });
            }

        }
        closeBtn.onClick.AddListener(OffUIBuilding);
    }

    void SetupUIViewBuildingInfo(ScriptableBuild build, UITransformBuild transformBuild)
    {
        CleanUIView();
        imgBuild.sprite = build.buildSprite;
        imgBuild.gameObject.SetActive(true);
        description.text = build.Decliption;
        nameBuild.text = build.NamedBuildTarget;
        countProduct.text = "Sản lượng mỗi ngày: " + build.CountProduct.ToString() ;
        typeBuild.text = "Loại công trình: Khai thác";
        
        if (build.ComboItemNeedBuild.Count > 0)
        {
            foreach (ComboItemNeed comboItemNeed in build.ComboItemNeedBuild)
            {
                GameObject newComboItem = Instantiate(prefabComboItemNeed, boxComboItemNeed);
                ScriptableObj findObj = UIManager.instance.GetImageObjByType(comboItemNeed.ItemType);
                newComboItem.gameObject.GetComponent<Image>().sprite = findObj.sprite;
                newComboItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = comboItemNeed.Count.ToString();
                comboItems.Add(newComboItem);
            }
        }

        if (CheckBuilding(build))
        {
            building.gameObject.SetActive(true);
            building.onClick.AddListener(() =>
            {
                RemoveAssetToBuild(build.ComboItemNeedBuild);
                
                transformBuild.Building(build);
                
                OffUIBuilding();
                
            });
            dialogErrorBuild.text = "";
        }
        else
        {
            building.gameObject.SetActive(false);
            dialogErrorBuild.text = "Thiếu nguyên liệu";
        }
        
    }

    void RemoveAssetToBuild(List<ComboItemNeed> comboItemNeeds)
    {
        foreach (ComboItemNeed comboItem in comboItemNeeds)
        {
            Player.instance.CheckRemoveAsset(comboItem.ItemType, 0, comboItem.Count);
        }
    }
    bool CheckBuilding(ScriptableBuild build)
    {
        foreach (ComboItemNeed comboItem in build.ComboItemNeedBuild)
        {
            if (comboItem.Count > Player.instance.GetResourceAmount(comboItem.ItemType))
            {
                return false;
            }
        }

        return true;
    }
    void CleanUIListView()
    {
        if (buttonList.Count > 0)
        {
            foreach (Button btn in buttonList)
            {
                btn.onClick.RemoveAllListeners();
            }
        }
        closeBtn.onClick.RemoveAllListeners();
    }
    void CleanUIView()
    {
        if (comboItems.Count > 0)
        {
            foreach (GameObject item in comboItems)
            {
                Destroy(item);
            }
            comboItems.Clear();
        }
        imgBuild.sprite = null;
        imgBuild.gameObject.SetActive(false);
        description.text = "";
        nameBuild.text = "";
        countProduct.text = "";
        typeBuild.text = "";
        dialogErrorBuild.text = "";
        building.onClick.RemoveAllListeners();
        building.gameObject.SetActive(false);
    }
    public void OnUIBuilding(UITransformBuild transformBuild)
    {
        gameObject.SetActive(true);
        InitListBuild(transformBuild);
    }

    public void OffUIBuilding()
    {
        CleanUIListView();
        CleanUIView();
        gameObject.SetActive(false);
    }
}
