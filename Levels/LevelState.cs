using System;
using System.Collections.Generic;
using System.Text;

namespace LudumDare46.Shared.Components
{
    public class LevelState
    {
        public bool IsBuildStage { get; set; }
        public bool IsPlayStage { get; set; }

        public int LevelNum {get; set; }
        public bool BuildDone { get; set; }
        public bool RestartDone { get; set; }
        public bool GameOver { get; set; }
        public float BuildingHealth { get; set; }
        public bool GameWon { get; set; }
        public bool ContinueDone { get; set; }
        public int SpawnsRemaining { get; set; }
    }
}
