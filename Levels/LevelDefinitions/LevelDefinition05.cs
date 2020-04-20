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

            list.AddRange(Enumerable.Range(0, 15)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 2,
                    Enemy = helper.Tank()
                }));

            list.AddRange(Enumerable.Range(0, 5)
                .Select(r => new EnemySpawnItem()
                {
                    SpawnTime = r * 2 + 20,
                    Enemy = helper.Tank()
                }));


            return list;
        }
    }
}