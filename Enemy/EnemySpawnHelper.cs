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
                    Speed = 30,
                    Armour = 1,
                    HP = 5,
                    Damage = 1
                };
        }

        public EnemyDefinition Car()
        {
            return new EnemyDefinition()
            {
                Appearance = EnemyAppearance.Car,
                Speed = 70,
                Armour = 2,
                HP = 20,
                Damage = 2
            };
        }

        public EnemyDefinition APC()
        {
            return new EnemyDefinition()
            {
                Appearance = EnemyAppearance.APC,
                Speed = 60,
                Armour = 5,
                HP = 25,
                Damage = 2
            };
        }

        public EnemyDefinition Tank()
        {
            return new EnemyDefinition()
            {
                Appearance = EnemyAppearance.Tank,
                Speed = 40,
                Armour = 10,
                HP = 30,
                Damage = 3
            };
        }

        public EnemyDefinition Truck()
        {
            return new EnemyDefinition()
            {
                Appearance = EnemyAppearance.Truck,
                Speed = 60,
                Armour = 2,
                HP = 25,
                Damage = 2
            };
        }
    }
}
