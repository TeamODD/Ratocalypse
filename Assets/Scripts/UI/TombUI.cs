using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace TeamOdd.Ratocalypse.UI
{
    public class TombUI : MonoBehaviour
    {
        public int TombDataCount { get; private set; }
        public TextMeshProUGUI TombText;

        public void Execute()
        {
            TombText.text = TombDataCount.ToString();
        }
    }
}