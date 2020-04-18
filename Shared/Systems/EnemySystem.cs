using System;
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
    internal class EnemySystem : EntityUpdateSystem
    {
        private ComponentMapper<EnemyComponent> _enemyMapper;
        private ComponentMapper<MovementComponent> _movementComponentMapper;

        private readonly TextureManager _textureManager;
        private readonly ViewportAdapter _viewportAdapter;

        private double TimeUntilNextSpawn = 0;

        public EnemySystem(TextureManager textureManager, ViewportAdapter viewportAdapter) : base(
            Aspect.All(typeof(EnemyComponent), typeof(MovementComponent)))
        {
            _textureManager = textureManager;
            _viewportAdapter = viewportAdapter;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _enemyMapper = mapperService.GetMapper<EnemyComponent>();
            _movementComponentMapper = mapperService.GetMapper<MovementComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            double time = gameTime.TotalGameTime.TotalSeconds;


        }
    }
}