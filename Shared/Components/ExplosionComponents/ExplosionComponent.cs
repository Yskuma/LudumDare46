using System;
using System.Collections.Generic;

using System.Text;
using Microsoft.Xna.Framework;

namespace LudumDare46.Shared.Components.explosionComponents
{
    class ExplosionComponent
    {
        public float MaxRadius { get; set; }
        public float CurrentRadius { get; set; }
        public float ExplosionSpeed { get; set; }
    }
}