using System;
using System.Collections.Generic;
using System.Text;

namespace LudumDare46.Enemy
{
    class EnemySpawnHelper
    {
        public EnemyDefinition Man()
        {
            return new EnemyDefinition()
                {
                    Appearance = EnemyAppearance.Man,
                    Speed = 20,
                    Armour = 0,
                    HP = 5
                };
        }

        public EnemyDefinition Car()
        {
            return new EnemyDefinition()
            {
                Appearance = EnemyAppearance.Car,
                Speed = 60,
                Armour = 2,
                HP = 20
            };
        }

        public EnemyDefinition APC()
        {
            return new EnemyDefinition()
            {
                Appearance = EnemyAppearance.APC,
                Speed = 50,
                Armour = 5,
                HP = 25
            };
        }

        public EnemyDefinition Tank()
        {
            return new EnemyDefinition()
            {
                Appearance = EnemyAppearance.Tank,
                Speed = 40,
                Armour = 10,
                HP = 30
            };
        }

        public EnemyDefinition Truck()
        {
            return new EnemyDefinition()
            {
                Appearance = EnemyAppearance.Truck,
                Speed = 50,
                Armour = 2,
                HP = 25
            };
        }
    }
}
