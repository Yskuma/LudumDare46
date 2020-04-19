using System;
using System.Diagnostics;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Components.BulletComponents;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.Systems.Bullet
{
    internal class BulletStopSystem : EntityUpdateSystem
    {
        private ComponentMapper<BulletComponent> _bulletMapper;
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<MovementComponent> _movementMapper;

        public BulletStopSystem() : base(
            Aspect.All(typeof(BulletComponent), typeof(Transform2), typeof(MovementComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _bulletMapper = mapperService.GetMapper<BulletComponent>();
            _transformMapper = mapperService.GetMapper<Transform2>();
            _movementMapper = mapperService.GetMapper<MovementComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);
                var movement = _movementMapper.Get(entity);
                var bullet = _bulletMapper.Get(entity);

                var distToTarget = Vector2.DistanceSquared(transform.Position, bullet.TargetPosition);
                var distToTravel = movement.Speed.LengthSquared() * MathF.Pow((float)gameTime.ElapsedGameTime.TotalSeconds,2);

                if (distToTarget < distToTravel)
                {
                    //Debug.WriteLine($"Bullet stopped at distance of {distToTarget} with travel dist of {distToTravel} | {a},{b}");
                    movement.Speed = Vector2.Zero;
                    transform.Position = new Vector2(bullet.TargetPosition.X, bullet.TargetPosition.Y);
                    bullet.AtTarget = true;
                }

            }

        }
    }
}