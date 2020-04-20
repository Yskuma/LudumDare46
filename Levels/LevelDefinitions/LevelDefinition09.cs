using System.Collections.Generic;
using System.Linq;
using LudumDare46.Enemy;

namespace LudumDare46.Levels.LevelDefinitions
{
    public class LevelDefinition09 : LevelDefinition
    {
        public LevelDefinition09()
        {
            Name = "Level 05";
            Map = "Level09";
            Resources = 100;
            Enemies = GenerateEnemies();
        }

        private List<EnemySpawnItem> GenerateEnemies()
        {
            var helper = new EnemySpawnHelper();

            var list = new List<EnemySpawnItem>();

            list.AddRange(Enumerable.Range(0, 250)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.2f,
                    Enemy = helper.Man()
                }));

            list.AddRange(Enumerable.Range(0, 50)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.5f + 5,
                    Enemy = helper.Car()
                }));

            list.AddRange(Enumerable.Range(0, 50)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 0.5f + 10,
                    Enemy = helper.Truck()
                }));

            list.AddRange(Enumerable.Range(0, 30)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * + 20,
                    Enemy = helper.Tank()
                }));

            return list;
        }
    }
}