using System;
using System.Collections.Generic;
using System.Text;
using LudumDare46.Shared.Components;
using MonoGame.Extended.Entities;

namespace LudumDare46.Levels
{
    public class Level
    {
        public World World { get; set; }

        public LevelState State { get; set; }
    }
}
