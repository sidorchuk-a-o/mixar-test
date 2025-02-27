using Game.Spaceships;
using UnityEngine;
using VContainer;

namespace Game.Battle
{
    public class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform shotSource;

        private WeaponData data;

        private IWeaponsManager weaponManager;

        private float rechargeTime;
        private float rechargeTimer;

        public int Id { get; private set; }
        public int Damage { get; private set; }
        public bool IsRecharging { get; private set; }

        public GameObject ProjectilePrefab { get; private set; }

        [Inject]
        public void Inject(IWeaponsManager weaponManager)
        {
            this.weaponManager = weaponManager;
        }

        public void Init(WeaponData data)
        {
            this.data = data;

            Id = data.Id;
            Damage = data.Damage;
            ProjectilePrefab = data.ProjectilePrefab;

            SetRechargeMod(0);

            IsRecharging = true;
            rechargeTimer = rechargeTime;
        }

        public void SetRechargeMod(float value)
        {
            rechargeTime = data.RechargeTime + data.RechargeTime * value;
        }

        public void TryShoot(Vector3 targetPosition)
        {
            if (IsRecharging)
            {
                return;
            }

            weaponManager.RegisterNewProjectile(
                weapon: this,
                sourcePosition: shotSource.transform.position,
                targetPosition: targetPosition);

            IsRecharging = true;
            rechargeTimer = rechargeTime;
        }

        private void Update()
        {
            if (!IsRecharging)
            {
                return;
            }

            rechargeTimer -= Time.deltaTime;

            if (rechargeTimer > 0)
            {
                return;
            }

            IsRecharging = false;
        }
    }
}