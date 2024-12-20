using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPrefabMessage : MonoBehaviour
{
    public UIPrefabText prefabText;
    
    public void InitMessage(PlayerRole playerRole, Color colorName, string txtName, string txtValue)
    {
        Debug.Log("d");
        if (playerRole == PlayerRole.Self)
        {
            gameObject.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
            
            UIPrefabText newNamePlayer = Instantiate(prefabText, transform);
            newNamePlayer.InitText(colorName, txtName);
            
            UIPrefabText message = Instantiate(prefabText, transform);
            message.InitText(Color.white, txtValue);
        }
        else if (playerRole == PlayerRole.Opponent)
        {
            gameObject.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.UpperRight;
            
            UIPrefabText newNamePlayer = Instantiate(prefabText, transform);
            newNamePlayer.InitText(colorName, txtName);
            
            UIPrefabText message = Instantiate(prefabText, transform);
            message.InitText(Color.white, txtValue);
        }
        else if (playerRole == PlayerRole.SelfError)
        {
            gameObject.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
            
            UIPrefabText message = Instantiate(prefabText, transform);
            message.InitText(Color.yellow, txtValue);
        }
        
    }
}
