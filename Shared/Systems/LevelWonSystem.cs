using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LudumDare46.Enemy;
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
    internal class LevelWonSystem : EntityUpdateSystem
    {
        private readonly LevelState _levelState;
        private ComponentMapper<EnemyComponent> _enemyMapper;

        public LevelWonSystem(LevelState levelState) : base(
            Aspect.All(typeof(EnemyComponent)))
        {
            _levelState = levelState;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _enemyMapper = mapperService.GetMapper<EnemyComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            if (ActiveEntities.IsEmpty && _levelState.SpawnsRemaining <= 0)
            {
                _levelState.GameWon = true;
            }
        }
    }
}