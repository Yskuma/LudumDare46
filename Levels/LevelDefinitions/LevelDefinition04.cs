using System.Collections.Generic;
using System.Linq;
using LudumDare46.Enemy;

namespace LudumDare46.Levels.LevelDefinitions
{
    public class LevelDefinition04 : LevelDefinition
    {
        public LevelDefinition04()
        {
            Name = "Level 04";
            Map = "Level04";
            Resources = 100;
            Enemies = GenerateEnemies();
        }

        private List<EnemySpawnItem> GenerateEnemies()
        {
            var helper = new EnemySpawnHelper();

            var list = new List<EnemySpawnItem>();

            list.AddRange(Enumerable.Range(0, 100)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.2f,
                    Enemy = helper.Man()
                }));

            list.AddRange(Enumerable.Range(0, 15)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.5f + 20f,
                    Enemy = helper.Car()
                }));

            list.Add(new EnemySpawnItem()
            {
                SpawnTime = 32,
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