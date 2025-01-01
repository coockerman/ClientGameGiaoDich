using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewListGround : MonoBehaviour
{
    public List<UITransformBuild> listUITransformBuild = new List<UITransformBuild>();

    public void UpdateListView(BuildData buildData)
    {
        int count = listUITransformBuild.Count;
        if (listUITransformBuild.Count >= buildData.comboBuilders.Count)
        {
            count = buildData.comboBuilders.Count;
        }
        
        for (int i = 0; i < count; i++)
        {
            if (buildData.comboBuilders[i] != null && listUITransformBuild[i] != null)
            {
                listUITransformBuild[i].UpdateUIBuild(buildData.comboBuilders[i]);
            }
        }
    }
    public void OnUIViewListGround()
    {
        gameObject.SetActive(true);
    }
}
