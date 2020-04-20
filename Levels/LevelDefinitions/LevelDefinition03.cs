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

            list.AddRange(Enumerable.Range(0, 40)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.5f,
                    Enemy = helper.Car()
                }));

            list.AddRange(Enumerable.Range(0, 30)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.5f + 10,
                    Enemy = helper.Truck()
                }));

            list.AddRange(Enumerable.Range(0, 20)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.5f + 20,
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