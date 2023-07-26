using TeamOdd.Ratocalypse.Card;
using TMPro;
using UnityEngine;

namespace TeamOdd.Ratocalypse.Ui
{
    public class DeckUi : MonoBehaviour
    {   
        public DeckData DeckData { get; private set; }
        public TextMeshProUGUI DeckText;

        public void Execute()
        {
            DeckText.text = DeckData.Count.ToString();
        }
    }
}
