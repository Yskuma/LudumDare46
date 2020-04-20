using System;
using System.Collections.Generic;
using System.Linq;
using LudumDare46.Shared;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Components.TurretComponents;
using LudumDare46.Shared.Enums;
using LudumDare46.Shared.Helpers;
using LudumDare46.Shared.Systems;
using LudumDare46.Shared.Systems.Bullet;
using LudumDare46.Shared.Systems.Gui;
using LudumDare46.Shared.Systems.Turret;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Gui;
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
            var guiSpriteBatchRenderer = new GuiSpriteBatchRenderer(graphicsDeviceManager.GraphicsDevice, () => Matrix.Identity);
            var worldBuilder = new WorldBuilder();

            var map = contentManager.Load<TiledMap>("Level01");

            var areaLayer = map.ObjectLayers.FirstOrDefault(r => r.Name == "Areas");

            var turretHelper = new TurretHelper();

            var spawnAreas = areaLayer.Objects
                .Where(r => r.Type == "Spawn")
                .Select(r => new Rectangle((int)r.Position.X, (int)r.Position.Y, (int)r.Size.Width, (int)r.Size.Height))
                .ToList();
            var damageAreas = areaLayer.Objects.Where(r => r.Type == "Damage")
                .Select(r => new Rectangle((int)r.Position.X, (int)r.Position.Y, (int)r.Size.Width, (int)r.Size.Height))
                .ToList();

            worldBuilder
                .AddSystem(new CleanupSystem(viewportAdapter))
                .AddSystem(new EnemySpawnSystem(textureManager, spawnAreas))
                .AddSystem(new EnemyCollisionSystem(damageAreas))
                .AddSystem(new BulletStopSystem())
                .AddSystem(new BulletDamageSystem())
                .AddSystem(new BulletCleanupSystem())
                .AddSystem(new EnemyDeathSystem())
                .AddSystem(new MovementSystem())
                .AddSystem(new TurretAimSystem(textureManager))
                .AddSystem(new RenderMapSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter, textureManager,
                    map))
                .AddSystem(new RenderSpriteSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter))
                .AddSystem(new TurretSpawnSystem(textureManager, turretHelper))
                .AddSystem(new GuiHandlerSystem(graphicsDeviceManager,viewportAdapter,guiSpriteBatchRenderer,contentManager,textureManager, turretHelper));


            var world = worldBuilder.Build();

            for (int x = 45; x < 63; x++)
            {
                for (int y = 4; y < 34; y++)
                {
                    turretHelper.TurretStats.Add(new TurretStat(x, y, TurretPart.Empty));
                }
            }

            /*
            var turret1 = world.CreateEntity();
            turret1.Attach(new Sprite(textureManager.Turret));
            turret1.Attach(new Transform2(720,192,0,1.0F,1.0F));
            turret1.Attach(new TurretComponent(400, 1.0f / 2)
            {
                PhysicalDamage = 5
            });

            var turret2 = world.CreateEntity();
            turret2.Attach(new Sprite(textureManager.Turret));
            turret2.Attach(new Transform2(720,352,0,1.0F,1.0F));
            turret2.Attach(new TurretComponent(400, 1.0f / 0.3f)
            {
                PhysicalDamage = 0.5f
            });

            var turret3 = world.CreateEntity();
            turret3.Attach(new Sprite(textureManager.Turret));
            turret3.Attach(new Transform2(720,512,0,1.0F,1.0F));
            turret3.Attach(new TurretComponent(400, 1.0f / 2)
            {
                PhysicalDamage = 5
            });
         */
        
            return world;
        }

    }
}