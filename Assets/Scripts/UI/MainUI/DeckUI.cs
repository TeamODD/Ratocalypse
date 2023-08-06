using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace TeamOdd.Ratocalypse.UI
{
    public class DeckUI : MonoBehaviour
    {
        private TextMeshProUGUI _deckText;

        public int DeckDataCount { get; private set; }

        public void Awake()
        {
            _deckText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        public void Execute()
        {
            _deckText.text = DeckDataCount.ToString();
        }

        public void ActivationChange() 
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
