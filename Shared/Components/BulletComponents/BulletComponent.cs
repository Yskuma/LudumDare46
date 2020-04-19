using System;
using System.Collections.Generic;

using System.Text;
using Microsoft.Xna.Framework;

namespace LudumDare46.Shared.Components.BulletComponents
{
    class BulletComponent
    {
        public float Radius { get; set; }
        public float PhysicalDamage { get; set; }
        public float ArmourPierce { get; set; }
        public float Speed { get; set; }

        public bool AtTarget { get; set; }

        public Vector2 TargetPosition { get; set; }
    }
}