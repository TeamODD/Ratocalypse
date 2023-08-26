using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.UI;
using UnityEngine;
using System.Linq;

public class CreatureUI : MonoBehaviour
{
    [SerializeField] // 테스트용 SerializeField
    private Transform _target;
    [SerializeField]
    private Vector3 _uiOffset;
    private RectTransform _rectTransform;
    private Vector3 _interfacePoint;  

    public HPUI _hpUI;
    public StaminaUI _staminaUI;
    public StatusUI _statusUI;

    private CreatureData _creatureData;
    private Renderer _renderer;
    
    public void AttachUi(Transform target, Vector3 offset, CreatureData data)
    {
        _hpUI = gameObject.GetComponent<HPUI>();
        _staminaUI = gameObject.GetComponent<StaminaUI>();
        _statusUI = gameObject.GetComponent<StatusUI>();

        _creatureData = data;
        _target = target;
        _uiOffset = offset;

        data.OnHpReduced.AddListener((_)=>{UpdateData();});
        data.OnHpRestored.AddListener((_)=>{UpdateData();});
        data.OnStaminaChanged.AddListener(UpdateData);
        data.OnEffectChanged.AddListener(UpdateData);
        data.OnDie.AddListener(()=>{gameObject.SetActive(false);});
        UpdateData();
    }

    private void UpdateData()
    {
        _hpUI.SetHpShield(_creatureData.Hp, _creatureData.MaxHp, _creatureData.Armor);
        _staminaUI.SetStamina(_creatureData.Stamina, _creatureData.MaxStamina);
        var statuses = _creatureData.StatusEffectList.Keys.ToList();
        _statusUI.RemoveAll();
        _statusUI.SetStatuses(statuses);
    }

    private void Start()
    {
        _rectTransform = transform.GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {

        // transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
        //transform.position = Camera.main.WorldToScreenPoint(target.position + new Vector3(0, 150, 0)) ;
        _interfacePoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _target.position);
        _rectTransform.anchoredPosition = _interfacePoint + _uiOffset;
    }
}
