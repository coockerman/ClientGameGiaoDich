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
    
    public Transform boxComboItemNeed;
    public GameObject prefabComboItemNeed;
    List<GameObject> comboItems = new List<GameObject>();

    public Button building;
    public Button closeBtn;

    void InitListBuild(UITransformBuild transformBuild)
    {
        CleanUIListView();
        if (isInit == false)
        {
            foreach (ScriptableBuild build in listBuild)
            {
                ScriptableBuild localBuild = build;
                Button newListBtn = Instantiate(prefabBtnBuilding, boxBtnBuilding);
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
        building.onClick.AddListener(() =>
        {
            transformBuild.Building(build);
            OffUIBuilding();
        });
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
        description.text = "";
        nameBuild.text = "";
        countProduct.text = "";
        typeBuild.text = "";
        building.onClick.RemoveAllListeners();
    }
    public void OnUIBuilding(UITransformBuild transformBuild)
    {
        gameObject.SetActive(true);
        InitListBuild(transformBuild);
    }

    public void OffUIBuilding()
    {
        gameObject.SetActive(false);
        CleanUIListView();
        CleanUIView();
    }
}
