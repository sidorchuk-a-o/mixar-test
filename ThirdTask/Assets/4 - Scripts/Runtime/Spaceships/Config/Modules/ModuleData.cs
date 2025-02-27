using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Spaceships
{
    public abstract class ModuleData : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string title;
        [SerializeField] protected string desc;

        [BoxGroup("Module (Prefab)"), HideLabel]
        [PreviewField(60, ObjectFieldAlignment.Left)]
        [SerializeField] private GameObject modulePrefab;

        public int Id => id;
        public string Title => title;
        public GameObject ModulePrefab => modulePrefab;

        public abstract string GetDesc();
    }
}