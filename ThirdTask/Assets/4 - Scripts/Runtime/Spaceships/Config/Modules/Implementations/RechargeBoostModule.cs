using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Spaceships
{
    [CreateAssetMenu(menuName = "Game/Modules/Recharge Boost")]
    public class RechargeBoostModule : ModuleData
    {
        [BoxGroup("Boost")]
        [SerializeField] private float value;

        public float Value => value;

        public override string GetDesc()
        {
            var sign = value > 0 ? "+" : "-";
            var percent = Mathf.RoundToInt(value * 100);

            return string.Format(desc, sign, percent);
        }
    }
}