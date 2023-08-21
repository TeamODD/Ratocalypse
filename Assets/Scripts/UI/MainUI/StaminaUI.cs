using System.Collections;
using System.Collections.Generic;
using System.Net;
using TeamOdd.Ratocalypse.CreatureLib.Cat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class StaminaUI : MonoBehaviour
{
    private Image _bigStaminaImage;
    private Image[] _smallStaminaImage = new Image[4];

    [SerializeField]
    private Sprite _bigBlueStamina;
    [SerializeField]
    private Sprite _bigGrayStamina;
    [SerializeField]
    private Sprite _smallBlueStamina;
    [SerializeField]
    private Sprite _smallGrayStamina;

    // 임시 테스트용 변수
    private int stamina = 5;

    private void SetStamina(int stamina)
    {
        if (stamina > 0)
            _bigStaminaImage.sprite = _bigBlueStamina;
        else
            _bigStaminaImage.sprite = _bigGrayStamina;

        int i;
        for (i = 0; i < stamina - 1; i++)
        {
            _smallStaminaImage[i].sprite = _smallBlueStamina;
        }
        for (; i < 4; i++)
        {
            _smallStaminaImage[i].sprite = _smallGrayStamina;
        }
    }

    void Awake()
    {
        _bigStaminaImage = transform.Find("Stamina/BigStamina").GetComponent<Image>();
        for (int i = 0; i < 4; i++)
        {
            _smallStaminaImage[i] = transform.Find("Stamina/SmallStaminaGroup/SmallStamina" + (i + 1)).GetComponent<Image>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (stamina == 0)
                stamina = 5;
            else stamina--;
            SetStamina(stamina);
        }
    }
}
