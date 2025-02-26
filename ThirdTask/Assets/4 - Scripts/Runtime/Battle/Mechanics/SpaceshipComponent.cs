﻿using Game.Spaceships;
using UnityEngine;

namespace Game.Battle
{
    public class SpaceshipComponent : MonoBehaviour
    {
        public int Id { get; private set; }
        public string Title { get; private set; }

        public void Init(SpaceshipData spaceshipData)
        {
            Id = spaceshipData.Id;
            Title = spaceshipData.Title;
        }
    }
}