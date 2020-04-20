using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace LudumDare46.Shared.Components
{
    public class AutoRemoveComponent
    {
        public float TimeAlive { get; set; }

        public AutoRemoveComponent(float time)
        {
            TimeAlive = time;
        }
    }
}
