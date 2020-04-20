using System;
using System.Diagnostics;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Components.BulletComponents;
using LudumDare46.Shared.Components.explosionComponents;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.Systems.Bullet
{
    internal class BulletStopSystem : EntityUpdateSystem
    {
        private readonly TextureManager _textureManager;
        private readonly SoundManager _soundManager;
        private ComponentMapper<BulletComponent> _bulletMapper;
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<MovementComponent> _movementMapper;

        public BulletStopSystem(TextureManager textureManager, SoundManager soundManager) : base(
            Aspect.All(typeof(BulletComponent), typeof(Transform2), typeof(MovementComponent)))
        {
            _textureManager = textureManager;
            _soundManager = soundManager;
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

                    var explosion = CreateEntity();
                    explosion.Attach(new Transform2(
                        new Vector2(transform.Position.X, transform.Position.Y),
                        0.0F,
                        Vector2.Zero
                        ));
                    explosion.Attach(new Sprite(_textureManager.BulletExplosion));
                    explosion.Attach(new ExplosionComponent()
                    {
                        CurrentRadius = 0.0f,
                        ExplosionSpeed = 500.0f,
                        MaxRadius = bullet.Radius
                    });

                    _soundManager.Explosion4.Play(0.2f, 0.0f, 0.0f);

                }

            }

        }
    }
}