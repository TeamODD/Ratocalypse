using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;


namespace TeamOdd.Ratocalypse.UI
{
    public class StaminaUI : MonoBehaviour
    {
        private int _maxStamina;
        [SerializeField]
        private Image _bigStaminaImage;
        private List<Image> _smallStaminaImage = new List<Image>();

        [SerializeField]
        private Sprite _bigBlueStaminaSprite;
        [SerializeField]
        private Sprite _bigGrayStaminaSprite;
        [SerializeField]
        private Sprite _smallBlueStaminaSprite;
        [SerializeField]
        private Sprite _smallGrayStaminaSprite;


        public void SetStamina(int stamina, int maxStamina)
        {

            var parent = transform.Find("Stamina/SmallStaminaGroup");
            _smallStaminaImage.Clear();
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
            SetMaxStamina(Mathf.Max(stamina,maxStamina));
            SetStamina(stamina);
        }

        public void SetMaxStamina(int maxStamina)
        {
            _maxStamina = maxStamina;

            for (int i = 0; i < maxStamina - 1; i++)
            {
                GameObject staminaUi = new GameObject();
                staminaUi.name = "SmallStamina";
                _smallStaminaImage.Add(staminaUi.AddComponent<Image>());
                _smallStaminaImage.Last().sprite = _bigBlueStaminaSprite;
                staminaUi.GetComponent<RectTransform>().SetParent(transform.Find("Stamina/SmallStaminaGroup"), false);
                staminaUi.SetActive(true);
            }
        }

        public void SetStamina(int stamina)
        {
            if (stamina > 0)
            {
                _bigStaminaImage.sprite = _bigBlueStaminaSprite;
            }
            else
            {
                _bigStaminaImage.sprite = _bigGrayStaminaSprite;
            }

            int i;
            for (i = 0; i < stamina - 1; i++)
            {
                _smallStaminaImage[i].sprite = _smallBlueStaminaSprite;
            }
            for (; i < _maxStamina - 1; i++)
            {
                _smallStaminaImage[i].sprite = _smallGrayStaminaSprite;
            }
        }

        private void Start()
        {

        }

        private void Update()
        {
        }
    }
}