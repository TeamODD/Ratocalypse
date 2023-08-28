using TeamOdd.Ratocalypse.CreatureLib;
using TeamOdd.Ratocalypse.UI;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using System.Collections.Generic;

public class CreatureUI : MonoBehaviour
{
    [SerializeField] // 테스트용 SerializeField
    public Transform _target;
    [SerializeField]
    private Vector3 _uiOffset;
    private RectTransform _rectTransform;
    private Vector3 _interfacePoint;  

    public HPUI _hpUI;
    public StaminaUI _staminaUI;
    public StatusUI _statusUI;

    private CreatureData _creatureData;
    private Renderer _renderer;

    [SerializeField]
    private float _modifyY;

    public void AttachUi(Transform target, Vector3 offset, CreatureData data)
    {
        _hpUI = gameObject.GetComponent<HPUI>();
        _staminaUI = gameObject.GetComponent<StaminaUI>();
        _statusUI = gameObject.GetComponent<StatusUI>();

        _creatureData = data;
        _target = target;
        _uiOffset = offset;
        _modifyY = 0;

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
        _rectTransform.anchoredPosition = _interfacePoint + _uiOffset + new Vector3(0, _modifyY, 0);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.transform.position.y >transform.position.y) { _modifyY -= 0.1f; }
        else { _modifyY += 0.1f; }
    }

}
