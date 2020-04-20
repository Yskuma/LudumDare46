using System;
using System.Net.NetworkInformation;
using LudumDare46.Shared.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Particles.Modifiers.Interpolators;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.Systems
{
    internal class RenderTextSystem : EntityDrawSystem
    {
        private GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;
        private ComponentMapper<TextComponent> _textMapper;
        private ComponentMapper<Transform2> _transformMapper;
        private readonly ViewportAdapter _viewportAdapter;
        private readonly TextureManager _textureManager;

        public RenderTextSystem(GraphicsDevice graphicsDevice, ViewportAdapter viewportAdapter, TextureManager textureManager)
            : base(Aspect.All(typeof(TextComponent), typeof(Transform2)))
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
                var text = _textMapper.Get(entity);
               
                int i = 0;
                _spriteBatch.DrawString(
                    spriteFont: _textureManager.FontArial,
                    text: text.Text,
                    position: transform.Position,
                    color: text.Colour,
                    rotation: transform.Rotation,
                    origin: Vector2.Zero, 
                    scale: transform.Scale,
                    effects: SpriteEffects.None,
                    layerDepth: 2.0f);
            }

            _spriteBatch.End();
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
            _transformMapper = mapperService.GetMapper<Transform2>();
            _textMapper = mapperService.GetMapper<TextComponent>();
        }
    }
}