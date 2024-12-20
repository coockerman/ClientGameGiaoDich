using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInfoVP : MonoBehaviour
{
    public TextMeshProUGUI txtInfoVP;
    private bool isOnTxt = false;


    public void OnInfoVP(string info, Vector3 position)
    {
        txtInfoVP.text = info;
        gameObject.transform.position = position;
        gameObject.SetActive(true);
    }

    public void OffInfoVP()
    {
        gameObject.SetActive(false);
    }
    
    
}
