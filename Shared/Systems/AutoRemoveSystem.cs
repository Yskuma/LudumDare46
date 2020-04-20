using LudumDare46.Shared.Components;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.Systems
{
    internal class AutoRemoveSystem : EntityUpdateSystem
    {
        private ComponentMapper<AutoRemoveComponent> _autoRemoveMapper;

        public AutoRemoveSystem()
            : base(Aspect.All(typeof(AutoRemoveComponent)))
        {

        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _autoRemoveMapper = mapperService.GetMapper<AutoRemoveComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var e in ActiveEntities)
            {
                var autoRemove = _autoRemoveMapper.Get(e);

                autoRemove.TimeAlive -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (autoRemove.TimeAlive <= 0)
                {
                    DestroyEntity(e);
                }

            }
        }
    }
}