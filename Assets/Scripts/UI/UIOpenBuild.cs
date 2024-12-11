using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIOpenBuild : MonoBehaviour
{
        public Button btnOpenBuild;
        public Button btnExit;
        public TextMeshProUGUI txtOrder;
        public TextMeshProUGUI txtTitle;
        public TextMeshProUGUI txtStatus;

        public void Init(string order, string title, string status, UnityAction openBuild)
        {
                Clear();
                txtOrder.text = order;
                txtTitle.text = title;
                txtStatus.text = status;
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
                txtOrder.text = "";
                txtTitle.text = "";
                txtStatus.text = "";
                btnOpenBuild.onClick.RemoveAllListeners();
                btnExit.onClick.RemoveAllListeners();
        }
}