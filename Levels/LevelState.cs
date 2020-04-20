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
    }
}
