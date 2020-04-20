using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Components.EnemyComponents;
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
        private List<Rectangle> _stopAreas;
        private LevelState _levelState;

        public EnemyCollisionSystem(List<Rectangle> stopAreas, LevelState levelState) : base(
            Aspect.All(typeof(EnemyComponent), typeof(Transform2), typeof(MovementComponent)))
        {
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
                        DestroyEntity(entity);
                    }
                }

            }

        }
    }
}