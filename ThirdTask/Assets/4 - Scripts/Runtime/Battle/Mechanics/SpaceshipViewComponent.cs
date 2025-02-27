using AD.ToolsCollection;
using Game.Spaceships;
using UniRx;
using UnityEngine;

namespace Game.Battle
{
    public class SpaceshipViewComponent : MonoBehaviour
    {
        private GameObject view;

        public void CreateView(SpaceshipData spaceshipData, SpaceshipComponent spaceship)
        {
            view = Instantiate(spaceshipData.SpaceshipPrefab, transform);

            spaceship.Actor.Health.IsAlive
                .Subscribe(x => view.SetActive(x))
                .AddTo(spaceship);
        }
    }
}