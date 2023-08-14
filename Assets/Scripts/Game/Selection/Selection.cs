using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TeamOdd.Ratocalypse.MapLib.MapData;

namespace TeamOdd.Ratocalypse.MapLib.GameLib.SelectionLib
{
    public class Selection<T> where T : IEnumerable
    {
        private T _candidates; 
        private Action<int> _selectCallback;
        private Action _cancelCallback;

        public bool Cancelable {get;}


        public Selection(T candidates, Action<int> selectCallback, Action cancelCallback = null)
        {
            _candidates = candidates;
            _selectCallback = selectCallback;
            _cancelCallback = cancelCallback;
            Cancelable = cancelCallback != null;
        }

        public void Select(int index)
        {
            _selectCallback?.Invoke(index);
        }

        public void Cancel()
        {
            if(!Cancelable)
            {
                throw new InvalidOperationException("This selection is not cancelable");
            }
            _cancelCallback?.Invoke();
        }

        public T GetCandidates()
        {
            return _candidates;
        }
    }

}