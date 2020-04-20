using System.Collections.Generic;
using System.Linq;
using LudumDare46.Enemy;

namespace LudumDare46.Levels.LevelDefinitions
{
    public class LevelDefinition07 : LevelDefinition
    {
        public LevelDefinition07()
        {
            Name = "Level 07";
            Map = "Level07";
            Resources = 100;
            Enemies = GenerateEnemies();
        }

        private List<EnemySpawnItem> GenerateEnemies()
        {
            var helper = new EnemySpawnHelper();

            var list = new List<EnemySpawnItem>();

            list.AddRange(Enumerable.Range(0, 80)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.25f,
                    Enemy = helper.Car()
                }));

            list.AddRange(Enumerable.Range(0, 60)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.25f + 10,
                    Enemy = helper.Truck()
                }));

            list.AddRange(Enumerable.Range(0, 20)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.5f + 20,
                    Enemy = helper.Tank()
                }));


            return list;
        }
    }
}