using LudumDare46.Shared;
using LudumDare46.Shared.Systems;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Levels.Level01
{
    public class Level01Factory : ILevel
    {
        public World Build(GraphicsDeviceManager graphicsDeviceManager, TextureManager _textureManager,
            ViewportAdapter viewportAdapter)
        {
            TextureManager textureManager = _textureManager;

            var world = new WorldBuilder();

            world
                .AddSystem(new CleanupSystem(viewportAdapter))
                .AddSystem(new EnemySystem(textureManager, viewportAdapter))
                .AddSystem(new MovementSystem())
                .AddSystem(new RenderSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter, _textureManager));

            return world.Build();
        }
    }
}