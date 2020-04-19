using System;
using System.Collections.Generic;
using System.Text;

namespace LudumDare46.Shared.Components.TurretComponents
{
    class TurretComponent
    {
        public float FireRate { get; set; }
        public float Radius { get; set; }
        public float PhysicalDamage { get; set; }
        public float Range { get; set; }
        public float ArmourPierce { get; set; }
        public float TargetRotation { get; set; }

        public double ReloadTimeRemaining { get; set; }

        public TurretComponent(float range, float fireRate)
        {
            Range = range;
            FireRate = fireRate;
            Radius = 64f;
        }
    }
}