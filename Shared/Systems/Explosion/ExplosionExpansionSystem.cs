using LudumDare46.Shared.Components.EnemyComponents;
using LudumDare46.Shared.Components.explosionComponents;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace LudumDare46.Shared.Systems.Explosion
{
    internal class ExplosionExpansionSystem : EntityUpdateSystem
    {
        private ComponentMapper<ExplosionComponent> _explosionMapper;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Transform2> _transformMapper;

        public ExplosionExpansionSystem() : base(
            Aspect.All(typeof(Sprite), typeof(ExplosionComponent), typeof(Transform2)))
        {

        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _explosionMapper = mapperService.GetMapper<ExplosionComponent>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
            _transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in ActiveEntities)
            {
                var explosion = _explosionMapper.Get(entity);
                var sprite = _spriteMapper.Get(entity);
                var transform = _transformMapper.Get(entity);

                explosion.CurrentRadius = explosion.CurrentRadius +
                                          ((float)gameTime.ElapsedGameTime.TotalSeconds * explosion.ExplosionSpeed);

                transform.Scale = new Vector2(explosion.CurrentRadius / (float)sprite.TextureRegion.Width, 
                    explosion.CurrentRadius / (float)sprite.TextureRegion.Height);
            }
        }
    }
}