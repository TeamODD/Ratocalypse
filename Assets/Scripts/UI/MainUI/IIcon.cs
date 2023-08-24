using TeamOdd.Ratocalypse.CreatureLib;
using UnityEngine;

namespace TeamOdd.Ratocalypse.UI
{
    public interface IIcon
    {
        public int Order { get; }
        public void SetPosition(Vector3 position);
    }
}