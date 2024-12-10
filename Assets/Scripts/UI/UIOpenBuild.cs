using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIOpenBuild : MonoBehaviour
{
        public Button btnOpenBuild;
        public Button btnExit;
        public TextMeshProUGUI txtTitle;

        public void Init(string title, UnityAction openBuild)
        {
                Clear();
                txtTitle.text = title;
                btnOpenBuild.onClick.AddListener(openBuild);
                btnExit.onClick.AddListener(OffOpenBuild);
        }

        public void OffOpenBuild()
        {
                gameObject.SetActive(false);
                Clear();
        }
        void Clear()
        {
                txtTitle.text = "";
                btnOpenBuild.onClick.RemoveAllListeners();
                btnExit.onClick.RemoveAllListeners();
        }
}