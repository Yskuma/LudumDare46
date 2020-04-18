using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.Systems
{
    internal class CleanupSystem : EntityUpdateSystem
    {
        private ComponentMapper<Transform2> _transformMapper;
        private readonly int _xMax;
        private readonly int _xMin;
        private readonly int _yMax;
        private readonly int _yMin;

        public CleanupSystem(ViewportAdapter viewportAdapter)
            : base(Aspect.All(typeof(Transform2)))
        {
            _xMin = -100;
            _xMax = viewportAdapter.ViewportWidth + 100;
            _yMin = -100;
            _yMax = viewportAdapter.ViewportHeight + 100;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var e in ActiveEntities)
            {
                var transform = _transformMapper.Get(e);

                if (
                    transform.Position.X < _xMin ||
                    transform.Position.X > _xMax ||
                    transform.Position.Y < _yMin ||
                    transform.Position.Y > _yMax)
                {
                    DestroyEntity(e);
                }
            }
        }
    }
}