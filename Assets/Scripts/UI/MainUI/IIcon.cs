using UnityEngine;

namespace TeamOdd.Ratocalypse.UI
{
    public interface IIcon
    {
        public int Order { get; set; }
        public void SetPosition(Vector3 position);

    }
}