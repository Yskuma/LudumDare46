using Microsoft.Xna.Framework;

namespace LudumDare46.Shared.Components
{
    internal class MovementComponent
    {
        public MovementComponent(Vector2 speed)
        {
            Speed = speed;
        }

        public Vector2 Speed { get; set; }
    }
}