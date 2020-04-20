using System.Collections.Generic;
using System.Linq;
using LudumDare46.Enemy;

namespace LudumDare46.Levels.LevelDefinitions
{
    public class LevelDefinition03 : LevelDefinition
    {
        public LevelDefinition03()
        {
            Name = "Level 03";
            Map = "Level03";
            Resources = 100;
            Enemies = GenerateEnemies();
        }

        private List<EnemySpawnItem> GenerateEnemies()
        {
            var helper = new EnemySpawnHelper();

            var list = new List<EnemySpawnItem>();

            list.AddRange(new []{0,2,4,6,8,10,12,14,16,18}
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r,
                    Enemy = helper.Car()
                }));

            list.AddRange(new []{20,22,24,26,28}
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r,
                    Enemy = helper.Truck()
                }));

            list.Add(new EnemySpawnItem()
            {
                SpawnTime = 35,
                Enemy = helper.Tank()
            });

            return list;
        }
    }
}