using System;
using LudumDare46.Shared.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.Systems
{
    internal class RenderMapSystem : DrawSystem
    {
        private GraphicsDevice _graphicsDevice;
        private readonly SpriteBatch _spriteBatch;
        private readonly ViewportAdapter _viewportAdapter;
        private TextureManager _textureManager;
        private TiledMapRenderer _mapRenderer;

        public RenderMapSystem(GraphicsDevice graphicsDevice, ViewportAdapter viewportAdapter,
            TextureManager textureManager, TiledMap map)
        {
            _graphicsDevice = graphicsDevice;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _viewportAdapter = viewportAdapter;
            _textureManager = textureManager;
            _mapRenderer = new TiledMapRenderer(_graphicsDevice, map);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: _viewportAdapter.GetScaleMatrix(), sortMode: SpriteSortMode.FrontToBack, samplerState: SamplerState.PointClamp);

            _mapRenderer.Draw(_viewportAdapter.GetScaleMatrix());

            _spriteBatch.End();
        }

    }
}