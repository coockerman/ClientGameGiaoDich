using System;
using System.Collections.Generic;
using UnityEngine;


public class Builder : MonoBehaviour
{
        public static Builder instance;
        private List<UITransformBuild> listUITransformBuild;
        private void Awake()
        {
                instance = this;
        }

        public float timeDay = 15f;
        private float countTimeDay = 0;

        private void Update()
        {
                countTimeDay += Time.deltaTime;
                if (countTimeDay >= timeDay)
                {
                        countTimeDay = 0;
                        Player.instance.UpDayPlayer();
                        HarvestBuild();
                }
        }

        void HarvestBuild()
        {
                listUITransformBuild = UIManager.instance.uiViewListGround.listUITransformBuild;
                foreach (UITransformBuild uiTransform in listUITransformBuild)
                {
                        if (uiTransform.scriptableBuild != null)
                        {
                                Player.instance.CheckAddAsset(
                                        uiTransform.scriptableBuild.ItemType,
                                        0,
                                        uiTransform.scriptableBuild.CountProduct
                                );
                        }
                }
        }

        public void OnBuilder()
        {
                gameObject.SetActive(true);
        }
}