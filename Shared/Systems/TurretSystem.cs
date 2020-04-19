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
    internal class TurretSystem : EntityProcessingSystem
    {
        private ComponentMapper<TurretComponent> _turretMapper;
        private ComponentMapper<Transform2> _transformMapper;

        private readonly TextureManager _textureManager;
        private readonly ViewportAdapter _viewportAdapter;

        private double TimeUntilNextSpawn = 0;

        public TurretSystem(TextureManager textureManager, ViewportAdapter viewportAdapter) : base(
            Aspect.All(typeof(TurretComponent), typeof(Transform2)))
        {
            _textureManager = textureManager;
            _viewportAdapter = viewportAdapter;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _turretMapper = mapperService.GetMapper<TurretComponent>();
            _transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Update(GameTime gameTime)
        {
            double time = gameTime.TotalGameTime.TotalSeconds;
        }

        public override void Process(GameTime gameTime, int entityId)
        {
            var transform = _transformMapper.Get(entityId);
            var gun = _turretMapper.Get(entityId);

            FaceNearestEnemy(transform);

        }

        private void FaceNearestEnemy(Transform2 transform)
        {
            transform.Rotation = (float)(1.5 * Math.PI);
        }
    }
}
