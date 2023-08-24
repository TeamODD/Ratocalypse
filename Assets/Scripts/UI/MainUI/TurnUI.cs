using System.Collections.Generic;
using TeamOdd.Ratocalypse.CreatureLib;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

namespace TeamOdd.Ratocalypse.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class TurnUI : MonoBehaviour
    {

        [SerializeField]
        private List<IIcon> _icons = new List<IIcon>();
        private List<int> _iconOrders = new List<int>();

        [SerializeField]
        private Vector2 _uiSize;

        [SerializeField]
        private float _xMargin = 50;

        [SerializeField]
        private float _maxXGap = 150;

        [SerializeField]
        private float _yGap = 100;

        [SerializeField]
        private float _yOrigin = 2;


        public List<GameObject> TestIconList; 

        private void Start()
        {
            _uiSize = GetComponent<RectTransform>().sizeDelta;
            SetTestIcon();
        }


        public void AddIcon(IIcon icon)
        {
            _icons.Add(icon);
        }

        public void SetTestIcon()
        {
            foreach (GameObject a in TestIconList)
            {
                _icons.Add(a.GetComponent<IIcon>());
            }
        }

        [ContextMenu("UpdatePositions")]
        public void UpdatePositions()
        {
            _iconOrders = _icons.Select((icon) => icon.Order).Distinct().ToList();
            _iconOrders.Sort();

            float xGap = (_uiSize.x - (2 * _xMargin)) / (_iconOrders.Count - 1);
            xGap = Mathf.Min(xGap, _maxXGap);

            float left = -(_uiSize.x / 2) + _xMargin;

            for (int xIndex = 0; xIndex < _iconOrders.Count; xIndex++)
            {
                int order = _iconOrders[xIndex];
                List<IIcon> iconsInOrder = _icons.Where((icon) => icon.Order == order).ToList();

                for (int yIndex = 0; yIndex < iconsInOrder.Count; yIndex++)
                {
                    IIcon icon = iconsInOrder[yIndex];
                    float x = left + (xGap * xIndex);
                    float y = _yOrigin -_yGap * yIndex;

                    icon.SetPosition(new Vector2(x, y));
                }
            }
        }


        public void ToggleActivation()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}