using System.Collections.Generic;
using UnityEngine;


namespace TeamOdd.Ratocalypse.UI
{
    public class RatList : MonoBehaviour
    {
        [SerializeField]
        private Vector2 _uiSize;

        [SerializeField]
        private float _yOrigin = 108;

        [SerializeField]
        private float _yGap = 20;

        [SerializeField]
        private float _xGap = 405;

        private List<MemberIcon> _memberIconlist = new List<MemberIcon>();

        public List<MemberIcon> MemberIconlist = new List<MemberIcon>();

        private void Start()
        {
            _uiSize = GetComponent<RectTransform>().sizeDelta;
        }

        public void MemberAdd(MemberIcon target) 
        {
            if (!MemberIconlist.Contains(target))
            {
                MemberIconlist.Add(target); 
            }
        }
        [ContextMenu("SetDeployment")]
        public void SetDeployment()
        {
            _memberIconlist.Clear();
            _memberIconlist = MemberIconlist.FindAll((member) => member.Activation == true);

            float vectorX;
            float vectorY;
            Vector3 position;
            for (int i=1; i<= MemberIconlist.Count;i++) 
            {
                if (MemberIconlist[i - 1].Activation)
                {
                    vectorY = (_yOrigin + _yGap) * (((float)(_memberIconlist.Count+1) / 2) - _memberIconlist.FindIndex(x => x== MemberIconlist[i - 1])-1);
                    position = new Vector3(0, vectorY, 0);
                    MemberIconlist[i - 1].SetMember(position);
                }
                else 
                {
                    vectorX = -_xGap;
                    position = new Vector3(vectorX, 0 , 0);
                    MemberIconlist[i - 1].SetMember(position);
                }

            }
        }
    }
}
