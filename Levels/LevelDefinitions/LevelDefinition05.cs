using System.Collections.Generic;
using System.Linq;
using LudumDare46.Enemy;

namespace LudumDare46.Levels.LevelDefinitions
{
    public class LevelDefinition05 : LevelDefinition
    {
        public LevelDefinition05()
        {
            Name = "Level 05";
            Map = "Level05";
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
                    Enemy = helper.Man()
                }));

            list.AddRange(new []{20,22,24,26,28}
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r,
                    Enemy = helper.Car()
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