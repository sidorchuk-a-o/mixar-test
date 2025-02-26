using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Spaceships
{
    [Serializable]
    public class SpaceshipData
    {
        [SerializeField] private int id;
        [SerializeField] private string title;

        [BoxGroup("Health"), HideLabel]
        [SerializeField] private HealthData health;

        [BoxGroup("Shield"), HideLabel]
        [SerializeField] private ShieldData shield;

        [BoxGroup("Weapons"), HideLabel]
        [SerializeField] private WeaponsData weapons;

        [BoxGroup("Modules"), HideLabel]
        [SerializeField] private ModulesData modules;

        [BoxGroup("Spaceship (View)")]
        [SerializeField] private Sprite spaceshipPreview;

        [BoxGroup("Spaceship (View)"), HideLabel]
        [PreviewField(150, ObjectFieldAlignment.Left)]
        [SerializeField] private GameObject spaceshipPrefab;

        public int Id => id;
        public string Title => title;

        public HealthData Health => health;
        public ShieldData Shield => shield;
        public WeaponsData Weapons => weapons;
        public ModulesData Modules => modules;

        public GameObject SpaceshipPrefab => spaceshipPrefab;
        public Sprite SpaceshipPreview => spaceshipPreview;
    }
}