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
    internal class EnemyDeathSystem : EntityUpdateSystem
    {
        private ComponentMapper<EnemyComponent> _enemyMapper;

        public EnemyDeathSystem() : base(
            Aspect.All(typeof(EnemyComponent)))
        {

        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _enemyMapper = mapperService.GetMapper<EnemyComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                var enemy = _enemyMapper.Get(entity);
                if (enemy.HP <= 0)
                {
                    DestroyEntity(entity);
                }
            }
        }
    }
}