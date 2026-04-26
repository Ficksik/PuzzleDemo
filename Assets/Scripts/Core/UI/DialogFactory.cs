using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using VContainer;

namespace PuzzleDemo.Core.UI
{
    public class DialogFactory
    {
        private readonly IObjectResolver _resolver;
        private readonly UIRoot _uiRoot;
        private readonly DialogPrefabRegistrySO _prefabRegistry;
        private readonly Dictionary<Type, IDialog> _pool = new();

        public DialogFactory(IObjectResolver resolver, UIRoot uiRoot, DialogPrefabRegistrySO prefabRegistry)
        {
            _resolver = resolver;
            _uiRoot = uiRoot;
            _prefabRegistry = prefabRegistry;
        }

        public T GetOrCreate<T>() where T : Component, IDialog
        {
            var type = typeof(T);

            if (_pool.TryGetValue(type, out var pooled) && pooled is T cached && cached != null)
            {
                return cached;
            }

            var prefab = _prefabRegistry.GetPrefab(type);
            if (prefab == null)
            {
                throw new InvalidOperationException($"Prefab not registered for dialog type {type.Name}");
            }

            var instance = UnityEngine.Object.Instantiate(prefab, _uiRoot.DialogContainer);
            instance.SetActive(false);

            var dialog = instance.GetComponent<T>();
            if (dialog == null)
            {
                throw new InvalidOperationException($"Dialog prefab for {type.Name} does not have component {type.Name}");
            }

            _resolver.Inject(dialog);
            _pool[type] = dialog;
            return dialog;
        }
    }
}
