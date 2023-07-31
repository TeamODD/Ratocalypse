using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace TeamOdd.Ratocalypse.UI
{
    public class DeckUI : MonoBehaviour
    {
        public int DeckDataCount { get; private set; }
        public TextMeshProUGUI DeckText { get; set; }
        public bool ActivationStatus { get; set; }

        public void Awake()
        {
            DeckText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        public void Execute()
        {
            DeckText.text = DeckDataCount.ToString();
        }

        public void Activation() 
        {
            gameObject.SetActive(ActivationStatus);
        }
    }
}
