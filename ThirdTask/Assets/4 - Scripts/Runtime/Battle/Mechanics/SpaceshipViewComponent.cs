using Game.Spaceships;
using UnityEngine;

namespace Game.Battle
{
    public class SpaceshipViewComponent : MonoBehaviour
    {
        public void CreateView(SpaceshipData spaceshipData)
        {
            Instantiate(spaceshipData.SpaceshipPrefab, transform);
        }
    }
}