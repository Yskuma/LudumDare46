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

            Random r = new Random(1983);

            for(int x = 0; x < 64; x++)
            {
                for (int y = 0; y < 40; y++)
                {
                    switch (r.Next(2))
                    {
                        case 0:
                            _spriteBatch.Draw(_textureManager.Dirt1, new Rectangle(x*16, y*16, 16, 16), Color.White);
                            break;
                        case 1:
                            _spriteBatch.Draw(_textureManager.Dirt2, new Rectangle(x*16, y*16, 16, 16), Color.White);
                            break;
                    }
                }

            }

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