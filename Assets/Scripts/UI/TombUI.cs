using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace TeamOdd.Ratocalypse.UI
{
    public class TombUI : MonoBehaviour
    { 
        private TextMeshProUGUI _tombText;

        public int TombDataCount { get; private set; }
        public void Awake()
        {
            _tombText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        public void Execute()
        {
            _tombText.text = TombDataCount.ToString();
        }

        public void ActivationChange()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}