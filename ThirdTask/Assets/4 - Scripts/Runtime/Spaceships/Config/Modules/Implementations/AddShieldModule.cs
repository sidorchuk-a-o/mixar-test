using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Spaceships
{
    [CreateAssetMenu(menuName = "Game/Modules/Add Shield")]
    public class AddShieldModule : ModuleData
    {
        [BoxGroup("Shield")]
        [SerializeField] private int value;

        public int Value => value;

        public override string GetDesc()
        {
            var sign = value > 0 ? "+" : "-";

            return string.Format(desc, sign, value);
        }
    }
}