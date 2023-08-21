using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TeamOdd.Ratocalypse.UI
{
    public class HPUI : MonoBehaviour
    {
        private Image _hpImage;
        private TextMeshProUGUI _hpText;

        private void SetHpText(string hpText)
        {
            _hpText.text = hpText;
        }

        private void UpdateHpBar(float hpRatio)
        {
            _hpImage.fillAmount = hpRatio;
        }


        void Awake()
        {
            _hpImage = transform.Find("HP/HpImage").GetComponent<Image>();
            _hpText = transform.Find("HP/HpText").GetComponent<TextMeshProUGUI>();
        }

        // Start is called before the first frame update
        void Start()
        {
            SetHpText("50 / 100");
            UpdateHpBar(0.5f);
        }
    }
}
