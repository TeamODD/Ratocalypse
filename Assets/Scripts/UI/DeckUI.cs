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
        private TextMeshProUGUI _deckText;
        public bool ActivationStatus { get; set; }

        public void Awake()
        {
            _deckText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        public void Execute()
        {
            _deckText.text = DeckDataCount.ToString();
        }

        public void Activation() 
        {
            gameObject.SetActive(ActivationStatus);
        }
    }
}
