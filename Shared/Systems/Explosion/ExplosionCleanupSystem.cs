using LudumDare46.Shared.Components.EnemyComponents;
using LudumDare46.Shared.Components.explosionComponents;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;

namespace LudumDare46.Shared.Systems.Explosion
{
    internal class ExplosionCleanupSystem : EntityUpdateSystem
    {
        private ComponentMapper<ExplosionComponent> _explosionMapper;

        public ExplosionCleanupSystem() : base(
            Aspect.All(typeof(ExplosionComponent)))
        {

        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _explosionMapper = mapperService.GetMapper<ExplosionComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                var explosion = _explosionMapper.Get(entity);
                if (explosion.CurrentRadius >= explosion.MaxRadius)
                {
                    DestroyEntity(entity);
                }
            }
        }
    }
}