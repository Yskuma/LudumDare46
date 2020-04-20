﻿using System.Linq;
using LudumDare46.Shared;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Enums;
using LudumDare46.Shared.Systems;
using LudumDare46.Shared.Systems.Bullet;
using LudumDare46.Shared.Systems.Explosion;
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
    public class LevelPlayFactory
    {
        public Level Build(GraphicsDeviceManager graphicsDeviceManager, TextureManager textureManager,
            ViewportAdapter viewportAdapter, ContentManager contentManager, TurretState turretState)
        {
            var guiSpriteBatchRenderer = new GuiSpriteBatchRenderer(graphicsDeviceManager.GraphicsDevice, () => Matrix.Identity);
            var worldBuilder = new WorldBuilder();

            var map = contentManager.Load<TiledMap>("Level01");

            var areaLayer = map.ObjectLayers.FirstOrDefault(r => r.Name == "Areas");
            turretState.TurretStats = turretState.TurretStats.Where(r => r.turretPart != TurretPart.Empty).ToList();
            turretState.TurretStats.ForEach(r => r.newPart = true);

            var levelState = new LevelState()
            {
                IsPlayStage = true,
                IsBuildStage = false,
                LevelNum = 1, 
                BuildingHealth = 100f
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
                .AddSystem(new EnemySpawnSystem(textureManager, spawnAreas))
                .AddSystem(new EnemyCollisionSystem(damageAreas))
                .AddSystem(new BulletStopSystem(textureManager))
                .AddSystem(new BulletDamageSystem())
                .AddSystem(new BulletCleanupSystem())
                .AddSystem(new ExplosionExpansionSystem())
                .AddSystem(new ExplosionCleanupSystem())
                .AddSystem(new EnemyDeathSystem())
                .AddSystem(new MovementSystem())
                .AddSystem(new TurretAimSystem(textureManager))
                .AddSystem(new RenderMapSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter, textureManager,
                    map))
                .AddSystem(new RenderSpriteSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter))
                .AddSystem(new TurretSpawnSystem(textureManager, turretState))
             .AddSystem(new PlayGuiHandlerSystem(graphicsDeviceManager,viewportAdapter,guiSpriteBatchRenderer,contentManager,textureManager, turretState, levelState));


            var world = worldBuilder.Build();
            var t = world.CreateEntity();

            var level = new Level()
            {
                World = world,
                LevelState = levelState,
                TurretState = turretState
            };
        
            return level;
        }

    }
}