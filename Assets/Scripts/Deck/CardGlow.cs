using System.Collections.Generic;
using TeamOdd.Ratocalypse.CardLib;
using UnityEngine;

namespace TeamOdd.Ratocalypse.DeckLib
{

    public class CardGlow : MonoBehaviour
    {
        [SerializeField]
        private Material _activeGlowMaterial;
        [SerializeField]
        private Material _inactiveGlowMaterial;
        [SerializeField]
        private Material _highlightGlowMaterial;
        [SerializeField]
        private Renderer _renderer;


        public void SetActiveGlow()
        {
            _renderer.material = _activeGlowMaterial;
        }

        public void SetInactiveGlow()
        {
            _renderer.material = _inactiveGlowMaterial;
        }

        public void SetHightLightGlow()
        {
            _renderer.material = _highlightGlowMaterial;
        }

    }
}