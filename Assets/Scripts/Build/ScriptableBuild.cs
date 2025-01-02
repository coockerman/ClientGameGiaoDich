using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Player/ScriptableBuild", fileName = "Build", order = 1)]
public class ScriptableBuild : ScriptableObject
{
        public string NamedBuildTarget;
        public string Decliption;
        public int CountProduct;
        public string nameProduct;
        
        public ItemType itemType;

        public string loaiCongTrinh;
        public float timeBuild;
        public Sprite btnSprite;
        public Sprite buildSprite;
        
        public List<ComboItem> ComboItemNeedBuild;
        
        public List<ComboItem> ComboItemNeedCreateProduct;
        
        public List<ComboItem> ComboItemSell;
}

[System.Serializable]
public class ComboItemNeed
{
        public ItemType ItemType;
        public int Count;
}

