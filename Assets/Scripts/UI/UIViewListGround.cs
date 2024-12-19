using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIViewListGround : MonoBehaviour
{
    public List<UITransformBuild> listUITransformBuild = new List<UITransformBuild>();

    public void OnUIViewListGround()
    {
        gameObject.SetActive(true);
    }
}
