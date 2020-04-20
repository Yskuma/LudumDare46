using System.Collections.Generic;
using System.Linq;
using LudumDare46.Enemy;

namespace LudumDare46.Levels.LevelDefinitions
{
    public class LevelDefinition08 : LevelDefinition
    {
        public LevelDefinition08()
        {
            Name = "Level 05";
            Map = "Level08";
            Resources = 100;
            Enemies = GenerateEnemies();
        }

        private List<EnemySpawnItem> GenerateEnemies()
        {
            var helper = new EnemySpawnHelper();

            var list = new List<EnemySpawnItem>();

            list.AddRange(Enumerable.Range(0, 150)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.15f,
                    Enemy = helper.Man()
                }));

            list.AddRange(Enumerable.Range(0, 20)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.4f + 20f,
                    Enemy = helper.Car()
                }));

            list.Add(new EnemySpawnItem()
            {
                SpawnTime = 32,
                Enemy = helper.Tank()
            });

            list.Add(new EnemySpawnItem()
            {
                SpawnTime = 33,
                Enemy = helper.Tank()
            });

            list.Add(new EnemySpawnItem()
            {
                SpawnTime = 34,
                Enemy = helper.Tank()
            });

            list.Add(new EnemySpawnItem()
            {
                SpawnTime = 35,
                Enemy = helper.Tank()
            });

            return list;
        }
    }
}