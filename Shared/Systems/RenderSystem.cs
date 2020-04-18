using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.Systems
{
    internal class RenderSystem : EntityDrawSystem
    {
        private GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;
        private ComponentMapper<Sprite> _spriteMapper;
        private ComponentMapper<Transform2> _transformMapper;
        private readonly ViewportAdapter _viewportAdapter;
        private TextureManager _textureManager;

        public RenderSystem(GraphicsDevice graphicsDevice, ViewportAdapter viewportAdapter, TextureManager textureManager)
            : base(Aspect.All(typeof(Sprite), typeof(Transform2)))
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _viewportAdapter = viewportAdapter;
            _textureManager = textureManager;
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: _viewportAdapter.GetScaleMatrix(), sortMode: SpriteSortMode.FrontToBack);

            foreach (var entity in ActiveEntities)
            {
                var transform = _transformMapper.Get(entity);
                var sprite = _spriteMapper.Get(entity);
                _spriteBatch.Draw(sprite, transform );
            }

            _spriteBatch.End();
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _spriteMapper = mapperService.GetMapper<Sprite>();
        }
    }
}