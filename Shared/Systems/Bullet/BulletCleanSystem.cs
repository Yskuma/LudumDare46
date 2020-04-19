using System;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Components.BulletComponents;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.Systems.Bullet
{
    internal class BulletCleanupSystem : EntityUpdateSystem
    {
        private ComponentMapper<BulletComponent> _bulletMapper;

        public BulletCleanupSystem() : base(
            Aspect.All(typeof(BulletComponent)))
        {
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _bulletMapper = mapperService.GetMapper<BulletComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                var bullet = _bulletMapper.Get(entity);

                if(bullet.AtTarget)
                {
                    DestroyEntity(entity);
                }
            }

        }
    }
}