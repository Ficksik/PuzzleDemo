using System;
using System.Linq;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "UI/Dialog Prefab Registry")]
    public class DialogPrefabRegistrySO : ScriptableObject
    {
        [System.Serializable]
        public class Entry
        {
            public string TypeName;
            public GameObject Prefab;
        }

        [SerializeField] private Entry[] _entries = System.Array.Empty<Entry>();

        public GameObject GetPrefab(Type type)
        {
            return _entries.FirstOrDefault(e => e.TypeName == type.Name)?.Prefab;
        }

        public void SetEntries(Entry[] entries)
        {
            _entries = entries;
        }
    }
}
