using TMPro;
using UnityEngine;

namespace TeamOdd.Ratocalypse.UI
{
    public class DeckUI : MonoBehaviour
    {
        public int DeckDataCount { get; private set; }
        public TextMeshProUGUI DeckText;

        public void Execute()
        {
            DeckText.text = DeckDataCount.ToString();
        }
    }
}
