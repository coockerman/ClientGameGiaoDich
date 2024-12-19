using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Player/ScriptableBuild", fileName = "Build", order = 1)]
public class ScriptableBuild : ScriptableObject
{
        public string NamedBuildTarget;
        public string Decliption;
        public int CountProduct;
        
        public TypeObj typeObj;
        public ItemType ItemType;
        public SolierType SolierType;
        
        public Sprite btnSprite;
        public Sprite buildSprite;
        
        public List<ComboItemNeed> ComboItemNeedBuild;
        public List<ComboSoliderNeed> ComboSoliderNeedBuild;
        
        public List<ComboItemNeed> ComboItemNeedCreateProduct;
        public List<ComboSoliderNeed> ComboSoliderNeedCreateProduct;
        
        public List<ComboItemNeed> ComboItemSell;
        public List<ComboSoliderNeed> ComboSoliderSell;
}

[System.Serializable]
public class ComboItemNeed
{
        public ItemType ItemType;
        public int Count;
}

[System.Serializable]
public class ComboSoliderNeed
{
        public SolierType SolierType;
        public int Count;
}