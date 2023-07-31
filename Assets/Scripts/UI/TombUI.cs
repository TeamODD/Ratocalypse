using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace TeamOdd.Ratocalypse.UI
{
    public class TombUI : MonoBehaviour
    { 
        public int TombDataCount { get; private set; }
        public TextMeshProUGUI TombText { get; set; }
        public bool ActivationStatus { get; set; }

        public void Awake()
        {
            TombText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        public void Execute()
        {
            TombText.text = TombDataCount.ToString();
        }

        public void Activation()
        {
            gameObject.SetActive(ActivationStatus);
        }
    }
}