using System;
using System.Collections.Generic;
using UnityEngine;


public class Builder : MonoBehaviour
{
        public List<ScriptableBuild> listBuild = new List<ScriptableBuild>();
        public List<UITransformBuild> listUITransformBuild = new List<UITransformBuild>();
        public float timeDay = 15f;
        private float countTimeDay = 0;
        public void SetupListBuild()
        {
                
        }

        private void Update()
        {
                countTimeDay += Time.deltaTime;
                if (countTimeDay >= timeDay)
                {
                        countTimeDay = 0;
                        Player.instance.Day += 1;
                }
        }

        void HarvestBuild()
        {
                
        }
}