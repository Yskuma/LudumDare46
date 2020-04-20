using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Components.EnemyComponents;
using LudumDare46.Shared.Components.explosionComponents;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.Systems
{
    internal class EnemyCollisionSystem : EntityUpdateSystem
    {
        private ComponentMapper<EnemyComponent> _enemyMapper;
        private ComponentMapper<Transform2> _transformMapper;
        private ComponentMapper<MovementComponent> _movementMapper;

        private double TimeUntilNextSpawn = 0;
        private readonly TextureManager _textureManager;
        private readonly SoundManager _soundManager;
        private List<Rectangle> _stopAreas;
        private LevelState _levelState;

        public EnemyCollisionSystem(TextureManager textureManager, SoundManager soundManager, List<Rectangle> stopAreas, LevelState levelState) : base(
            Aspect.All(typeof(EnemyComponent), typeof(Transform2), typeof(MovementComponent)))
        {
            _textureManager = textureManager;
            _soundManager = soundManager;
            _stopAreas = stopAreas;
            _levelState = levelState;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _enemyMapper = mapperService.GetMapper<EnemyComponent>();
            _transformMapper = mapperService.GetMapper<Transform2>();
            _movementMapper = mapperService.GetMapper<MovementComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);
                var movement = _movementMapper.Get(entity);

                foreach (var stopArea in _stopAreas)
                {
                    if (transform.Position.X > stopArea.X &&
                        transform.Position.X < stopArea.X + stopArea.Width &&
                        transform.Position.Y > stopArea.Y &&
                        transform.Position.Y < stopArea.Y + stopArea.Height)
                    {
                        //movement.Speed = Vector2.Zero;
                        _levelState.BuildingHealth = _levelState.BuildingHealth - 1;

                        var explosion = CreateEntity();
                        explosion.Attach(new Transform2(
                            new Vector2(transform.Position.X, transform.Position.Y),
                            0.0F,
                            Vector2.Zero
                        ));
                        explosion.Attach(new Sprite(_textureManager.EnemyExplosion));
                        explosion.Attach(new ExplosionComponent()
                        {
                            CurrentRadius = 0.0f,
                            ExplosionSpeed = 500.0f,
                            MaxRadius = 200.0f
                        });

                        _soundManager.Explosion2.Play(0.5f, 0,0);

                        DestroyEntity(entity);
                    }
                }

            }

        }
    }
}