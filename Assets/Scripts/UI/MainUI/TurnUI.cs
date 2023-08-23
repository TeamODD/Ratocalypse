using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace TeamOdd.Ratocalypse.UI
{
    public class TurnUI : MonoBehaviour
    {
       
        private List<IIcon> _deployment = new List<IIcon>();

        [SerializeField]
        private List<int> _deploymentNumber = new List<int>();

        private Vector2 _uiSize;

        [SerializeField]
        private float _xMargin = 50;
        [SerializeField]
        private float _maxXGap = 150;

        [SerializeField]
        private float _yGap = 30;

        [SerializeField]
        private float _yOrigin = 30;

        public List<GameObject> TestIconList; 


        private void Start()
        {
            _uiSize = GetComponent<RectTransform>().sizeDelta;
            SetTestIcon();
        }
        public void AddIcon(IIcon icon)
        {
            _deployment.Add(icon);
        }
        public void SetTestIcon()
        {
            foreach (GameObject a in TestIconList)
            {
                _deployment.Add(a.GetComponent<IIcon>());
            }
        }

        [ContextMenu("SetDeployment")]
        public void SetDeployment()
        {
            _deploymentNumber = _deployment.Select((icon) => icon.Order).Distinct().ToList();
            _deploymentNumber = _deploymentNumber.FindAll((oreder) => oreder != -1).Distinct().ToList();
            _deploymentNumber.Sort();

            float interval = Mathf.RoundToInt((_uiSize.x - 20) / _deploymentNumber.Count);
            interval = Mathf.Min(interval, _maxXGap);

            float left = -(_uiSize.x / 2) + _xMargin;

            foreach (IIcon a in _deployment)
            {
                List<IIcon> iconsInOrder = _deployment.Where((icon) => icon.Order == a.Order).ToList();

             
                if (a.Order == -1)
                {
                    float x = (_uiSize.x - 20) / 2;
                    float y = -_yOrigin -_yGap * iconsInOrder.FindIndex((icon) => icon == a);

                    a.SetPosition(new Vector2(x,y));
                }
                else
                {
                    float x = left + (interval * _deploymentNumber.FindIndex(element => element == a.Order));
                    float y = -_yOrigin - _yGap * iconsInOrder.FindIndex((icon) => icon == a);

                    a.SetPosition(new Vector2(x, y));
                }
            }
        }

        public void ActivationChange()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}