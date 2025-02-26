using Game.Spaceships;
using UnityEngine;

namespace Game.Battle
{
    public class SpaceshipViewComponent : MonoBehaviour
    {
        public void Init(SpaceshipData spaceshipData)
        {
            Instantiate(spaceshipData.SpaceshipPrefab, transform);
        }
    }
}