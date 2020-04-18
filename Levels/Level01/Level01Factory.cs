using System;
using System.Collections.Generic;
using LudumDare46.Shared;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Levels.Level01
{
    public class Level01Factory : ILevel
    {
        public World Build(GraphicsDeviceManager graphicsDeviceManager, TextureManager textureManager,
            ViewportAdapter viewportAdapter, ContentManager contentManager)
        {
            var worldBuilder = new WorldBuilder();

            var map = contentManager.Load<TiledMap>("Level01");

            worldBuilder
                .AddSystem(new CleanupSystem(viewportAdapter))
                .AddSystem(new EnemySystem(textureManager, viewportAdapter))
                .AddSystem(new MovementSystem())
                .AddSystem(new RenderMapSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter, textureManager, map));
                //.AddSystem(new RenderSpriteSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter, textureManager));

            var world = worldBuilder.Build();

            var s = map.ObjectLayers[0].Objects[0].Size;
            var p = map.ObjectLayers[0].Objects[0].Position;

            Console.WriteLine($"Size is {s.Width},{s.Height}");
            Console.WriteLine($"Position is {p.X},{p.Y}");
            return world;
        }

    }
}