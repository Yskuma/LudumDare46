using LudumDare46.Shared.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace LudumDare46.Shared.Systems
{
    internal class MovementSystem : EntityUpdateSystem
    {
        private ComponentMapper<MovementComponent> _movementMapper;
        private ComponentMapper<Transform2> _transformMapper;

        public MovementSystem()
            : base(Aspect.All(typeof(Transform2), typeof(MovementComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _movementMapper = mapperService.GetMapper<MovementComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var e in ActiveEntities)
            {
                var transform = _transformMapper.Get(e);
                var movement = _movementMapper.Get(e);

                transform.Position = new Vector2(
                    transform.Position.X + movement.Speed.X * (float) gameTime.ElapsedGameTime.TotalSeconds,
                    transform.Position.Y + movement.Speed.Y * (float) gameTime.ElapsedGameTime.TotalSeconds
                );
            }
        }
    }
}