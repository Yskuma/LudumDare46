using System.Collections.Generic;
using LudumDare46.Shared;
using LudumDare46.Shared.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Levels.Level01
{
    public class Level01Factory : ILevel
    {
        public World Build(GraphicsDeviceManager graphicsDeviceManager, TextureManager textureManager,
            ViewportAdapter viewportAdapter)
        {
            var worldBuilder = new WorldBuilder();

            worldBuilder
                .AddSystem(new CleanupSystem(viewportAdapter))
                .AddSystem(new EnemySystem(textureManager, viewportAdapter))
                .AddSystem(new MovementSystem())
                .AddSystem(new RenderSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter, textureManager));

            var world = worldBuilder.Build();

            var back = world.CreateEntity();
            back.Attach(new Sprite(BackgroundTexture(graphicsDeviceManager.GraphicsDevice, textureManager)));
            back.Attach(new Transform2(new Vector2(512, 320)));

            return world;
        }

        private Texture2D BackgroundTexture(GraphicsDevice graphicsDevice, TextureManager textureManager)
        {
            var tex = new RenderTarget2D(graphicsDevice, 16 * 64, 16 * 40);
            graphicsDevice.SetRenderTarget(tex);
            var spriteBatch = new SpriteBatch(graphicsDevice);

            Dictionary<char, TextureRegion2D> map = new Dictionary<char, TextureRegion2D>();

            map.Add('.', textureManager.Dirt1);
            map.Add(',', textureManager.Dirt2);
            map.Add('*', textureManager.RoofTile);
            map.Add('|', textureManager.RoofEdge);

            spriteBatch.Begin();

            var layout = LayoutString();
            for (int y = 0; y < layout.Length; y++)
            {
                for (int x = 0; x < layout[y].Length; x++)
                {
                    var current = layout[y][x];
                    if (map.ContainsKey(current))
                    {
                        spriteBatch.Draw(map[current], new Rectangle(x * 16, y * 16, 16, 16), Color.White);
                    }
                }
            }

            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            return tex;
        }


        private string[] LayoutString()
        {
            return new string[]
            {
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************",
                "...........................................|********************"
            };
        }
    }
}