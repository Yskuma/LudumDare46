using System;
using System.Collections.Generic;
using System.Text;
using LudumDare46.Enemy;

namespace LudumDare46.Levels
{
    public abstract class LevelDefinition
    {
        public string Name { get; set; }
        public string Map { get; set; }
        public int Resources { get; set; }

        public List<EnemySpawnItem> Enemies { get; set; }
    }
}
