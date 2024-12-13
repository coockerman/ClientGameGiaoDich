using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPrefabMessage : MonoBehaviour
{
    public UIPrefabText prefabText;
    
    public void InitMessage(PlayerRole playerRole, Color colorName, string txtName, string txtValue)
    {
        if (playerRole == PlayerRole.Self)
        {
            gameObject.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
            
            UIPrefabText newNamePlayer = Instantiate(prefabText, transform);
            newNamePlayer.InitText(colorName, txtName);
            
            UIPrefabText message = Instantiate(prefabText, transform);
            message.InitText(Color.black, txtValue);
        }
        else if (playerRole == PlayerRole.Opponent)
        {
            gameObject.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.UpperRight;
            
            
            UIPrefabText message = Instantiate(prefabText, transform);
            message.InitText(Color.black, txtValue);
            
            UIPrefabText newNamePlayer = Instantiate(prefabText, transform);
            newNamePlayer.InitText(colorName, txtName);
        }
        
    }
}
