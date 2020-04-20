using System.Linq;
using LudumDare46.Shared;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Enums;
using LudumDare46.Shared.Helpers;
using LudumDare46.Shared.Systems;
using LudumDare46.Shared.Systems.Bullet;
using LudumDare46.Shared.Systems.Gui;
using LudumDare46.Shared.Systems.Turret;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Levels
{
    public class LevelBuildFactory
    {
        public Level Build(GraphicsDeviceManager graphicsDeviceManager, TextureManager textureManager,
            ViewportAdapter viewportAdapter, ContentManager contentManager)
        {
            var guiSpriteBatchRenderer = new GuiSpriteBatchRenderer(graphicsDeviceManager.GraphicsDevice, () => Matrix.Identity);
            var worldBuilder = new WorldBuilder();

            var map = contentManager.Load<TiledMap>("Level01");

            var areaLayer = map.ObjectLayers.FirstOrDefault(r => r.Name == "Areas");

            var turretHelper = new TurretHelper();

            var state = new LevelState()
            {
                IsPlayStage = false,
                IsBuildStage = true,
                LevelNum = 1
            };

            var spawnAreas = areaLayer.Objects
                .Where(r => r.Type == "Spawn")
                .Select(r => new Rectangle((int)r.Position.X, (int)r.Position.Y, (int)r.Size.Width, (int)r.Size.Height))
                .ToList();
            var damageAreas = areaLayer.Objects.Where(r => r.Type == "Damage")
                .Select(r => new Rectangle((int)r.Position.X, (int)r.Position.Y, (int)r.Size.Width, (int)r.Size.Height))
                .ToList();

            worldBuilder
                .AddSystem(new CleanupSystem(viewportAdapter))
                .AddSystem(new MovementSystem())
                .AddSystem(new RenderMapSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter, textureManager,
                    map))
                .AddSystem(new RenderSpriteSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter))
                .AddSystem(new TurretSpawnSystem(textureManager, turretHelper))
                .AddSystem(new GuiHandlerSystem(graphicsDeviceManager,viewportAdapter,guiSpriteBatchRenderer,contentManager,textureManager, turretHelper,state));

            var world = worldBuilder.Build();

            for (int x = 45; x < 63; x++)
            {
                for (int y = 4; y < 34; y++)
                {
                    turretHelper.TurretStats.Add(new TurretStat(x, y, TurretPart.Empty));
                }
            }

            var level = new Level()
            {
                World = world,
                State = state
            };
        
            return level;
        }

    }
}