using System;
using UnityEngine;

namespace PuzzleDemo.Core.UI
{
    public class LoadingOverlay : MonoBehaviour, ILoadingOverlay
    {
        [SerializeField] private GameObject _root;

        private int _counter;

        private void Awake()
        {
            if (_root == null) _root = gameObject;
            _root.SetActive(false);
        }

        public IDisposable Show()
        {
            _counter++;
            UpdateVisibility();
            return new Scope(this);
        }

        private void Release()
        {
            if (_counter <= 0) return;
            _counter--;
            UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            // Always keep overlay on top of any newly spawned dialog.
            if (_counter > 0)
            {
                _root.transform.SetAsLastSibling();
                _root.SetActive(true);
            }
            else
            {
                _root.SetActive(false);
            }
        }

        private sealed class Scope : IDisposable
        {
            private LoadingOverlay _owner;

            public Scope(LoadingOverlay owner) => _owner = owner;

            public void Dispose()
            {
                if (_owner == null) return;
                _owner.Release();
                _owner = null;
            }
        }
    }
}
