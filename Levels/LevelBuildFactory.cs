﻿using System.Linq;
using LudumDare46.Shared;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Enums;
using LudumDare46.Shared.Systems;
using LudumDare46.Shared.Systems.Bullet;
using LudumDare46.Shared.Systems.Gui;
using LudumDare46.Shared.Systems.Turret;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Levels
{
    public class LevelBuildFactory
    {
        public Level Build(GraphicsDeviceManager graphicsDeviceManager, TextureManager textureManager,
            SoundManager soundManager,
            ViewportAdapter viewportAdapter, ContentManager contentManager, LevelDefinition levelDefinition)
        {
            var guiSpriteBatchRenderer = new GuiSpriteBatchRenderer(graphicsDeviceManager.GraphicsDevice, () => Matrix.Identity);
            var worldBuilder = new WorldBuilder();

            var map = contentManager.Load<TiledMap>(levelDefinition.Map);

            var areaLayer = map.ObjectLayers.FirstOrDefault(r => r.Name == "Areas");

            var turretState = new TurretState();

            var levelState = new LevelState()
            {
                IsPlayStage = false,
                IsBuildStage = true,
                LevelNum = 1,
                Name =  levelDefinition.Name
            };

            var spawnAreas = areaLayer.Objects
                .Where(r => r.Type == "Spawn")
                .Select(r => new Rectangle((int)r.Position.X, (int)r.Position.Y, (int)r.Size.Width, (int)r.Size.Height))
                .ToList();
            var damageAreas = areaLayer.Objects.Where(r => r.Type == "Damage")
                .Select(r => new Rectangle((int)r.Position.X, (int)r.Position.Y, (int)r.Size.Width, (int)r.Size.Height))
                .ToList();

            var buildAreas = areaLayer.Objects.Where(r => r.Type == "BuildArea")
                .Select(r => new Rectangle((int)r.Position.X, (int)r.Position.Y, (int)r.Size.Width, (int)r.Size.Height))
                .ToList();

            worldBuilder
                .AddSystem(new CleanupSystem(viewportAdapter))
                .AddSystem(new MovementSystem())
                .AddSystem(new RenderMapSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter, textureManager,
                    map))
                .AddSystem(new RenderSpriteSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter))
                .AddSystem(new TurretSpawnSystem(textureManager, turretState))
                .AddSystem(new BuildGuiHandlerSystem(graphicsDeviceManager,viewportAdapter,guiSpriteBatchRenderer,contentManager,textureManager, turretState,levelState, buildAreas));

            var world = worldBuilder.Build();

            var level = new Level()
            {
                World = world,
                LevelState = levelState,
                TurretState = turretState
            };

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(soundManager.ChillStep2);
        
            return level;
        }

    }
}