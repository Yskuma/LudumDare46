using System.Collections.Generic;
using System.Linq;
using LudumDare46.Enemy;
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
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Levels
{
    public class LevelMainMenuFactory
    {
        public Level Build(GraphicsDeviceManager graphicsDeviceManager, TextureManager textureManager,
            SoundManager soundManager,
            ViewportAdapter viewportAdapter, ContentManager contentManager)
        {
            var guiSpriteBatchRenderer = new GuiSpriteBatchRenderer(graphicsDeviceManager.GraphicsDevice, () => Matrix.Identity);
            var worldBuilder = new WorldBuilder();

            var map = contentManager.Load<TiledMap>("Level01");

            var levelState = new LevelState()
            {
                IsPlayStage = false,
                IsBuildStage = false,
                LevelNum = 0, 
                BuildingHealth = 10f
            };

            worldBuilder
                .AddSystem(new CleanupSystem(viewportAdapter))
                .AddSystem(new RenderMapSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter, textureManager,
                    map))
                
                .AddSystem(new MainMenuGuiHandlerSystem(graphicsDeviceManager,viewportAdapter,guiSpriteBatchRenderer,contentManager,textureManager, levelState))
                .AddSystem(new RenderSpriteSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter))
                .AddSystem(new RenderTextSystem(graphicsDeviceManager.GraphicsDevice, viewportAdapter,textureManager));

            var world = worldBuilder.Build();

            var t = world.CreateEntity();
            t.Attach(new TextComponent(){Colour = Color.White, Text = "A big thanks to Kenney (Graphics & Sound) and Pudgyplatypus (Music)"});
            t.Attach(new Transform2(new Vector2(viewportAdapter.ViewportHeight/ 2,viewportAdapter.ViewportHeight - 20)));

            var level = new Level()
            {
                World = world,
                LevelState = levelState
            };



            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(soundManager.FlowingRocks);
        
            return level;
        }

    }
}