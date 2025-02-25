using UnityEngine;

namespace Game.Spaceships
{
    public abstract class ModuleData : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string title;
        [SerializeField] protected string desc;

        public int Id => id;
        public string Title => title;

        public abstract string GetDesc();
    }
}