using System;
using System.Collections.Generic;
using System.Text;

namespace LudumDare46.Enemy
{
    public class EnemyDefinition
    {
        public float HP { get; set; }
        public float Armour { get; set; }
        public float Speed { get; set; }
        public EnemyAppearance Appearance { get; set; }
    }
}
