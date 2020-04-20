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

            list.AddRange(Enumerable.Range(0, 50)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.25f,
                    Enemy = helper.Man()
                }));

            list.AddRange(Enumerable.Range(0, 50)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.25f,
                    Enemy = helper.Man()
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