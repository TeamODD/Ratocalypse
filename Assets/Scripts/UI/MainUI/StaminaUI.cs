using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    private Image _bigStaminaImage;
    private Image[] _smallStaminaImage = new Image[4];

    [SerializeField]
    private Sprite _bigBlueStaminaSprite;
    [SerializeField]
    private Sprite _bigGrayStaminaSprite;
    [SerializeField]
    private Sprite _smallBlueStaminaSprite;
    [SerializeField]
    private Sprite _smallGrayStaminaSprite;


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
        for (; i < 4; i++)
        {
            _smallStaminaImage[i].sprite = _smallGrayStaminaSprite;
        }
    }

    private void Awake()
    {
        _bigStaminaImage = transform.Find("Stamina/BigStamina").GetComponent<Image>();
        for (int i = 0; i < 4; i++)
        {
            _smallStaminaImage[i] = transform.Find("Stamina/SmallStaminaGroup/SmallStamina" + (i + 1)).GetComponent<Image>();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetStamina(3);
    }

    private void Update()
    {
    }
}
