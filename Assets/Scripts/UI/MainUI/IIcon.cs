using TeamOdd.Ratocalypse.CreatureLib;
using UnityEngine;

namespace TeamOdd.Ratocalypse.UI
{
    public interface IIcon
    {
        public int Order { get; }
        public void Remove();
        public void SetPosition(int order, Vector3 position);
    }
}