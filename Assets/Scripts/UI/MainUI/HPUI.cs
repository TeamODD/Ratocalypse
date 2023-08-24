using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    private Image _hpImage;
    private TextMeshProUGUI _hpText;

    [SerializeField]
    private Sprite _redHpSprite;
    [SerializeField]
    private Sprite _blueHpSprite;

    [SerializeField]
    private int _currentHp;
    [SerializeField]
    private int _maxHp;
    [SerializeField]
    private int _currentShield;

    public void SetHpShield(int currentHp, int maxHp, int shield)
    {
        _currentHp = currentHp;
        _maxHp = maxHp;
        _currentShield = shield;

        if (shield > 0)
        {
            _hpImage.sprite = _blueHpSprite;
        }
        else
        {
            _hpImage.sprite = _redHpSprite;
        }

        _hpText.text = currentHp + (shield > 0 ? "(+" + shield + ")" : "") + "/" + maxHp;
        _hpImage.fillAmount = (float)currentHp / maxHp;
    }

    private void Awake()
    {
        _hpImage = transform.Find("HP/HpImage").GetComponent<Image>();
        _hpText = transform.Find("HP/HpText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetHpShield(5, 10, 3);
    }

    [ContextMenu("ExcuteEvent")]
    private void TestHpShield()
    {
        SetHpShield(_currentHp, _maxHp, _currentShield);
    }
}
