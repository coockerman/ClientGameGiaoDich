using UnityEngine;

[CreateAssetMenu(fileName = "Obj", menuName = "Player/Obj", order = 2)]
public class ScriptableObj : ScriptableObject
{
        public TypeObj typeObj;
        public ItemType itemType;
        public SolierType solierType;
        public Sprite sprite;
        public string nameObj;
}