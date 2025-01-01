using UnityEngine;

[CreateAssetMenu(fileName = "Obj", menuName = "Player/Obj", order = 2)]
public class ScriptableObj : ScriptableObject
{
        public ItemType itemType;
        public Sprite sprite;
        public string nameObj;
}