using System;
using System.Collections.Generic;
using System.ComponentModel;
using LudumDare46.Levels;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.Systems.Gui
{
    class PlayGuiHandlerSystem : IUpdateSystem, IDrawSystem
    {
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly ViewportAdapter _defaultViewportAdapter;
        private readonly GuiSpriteBatchRenderer _guiSpriteBatchRenderer;
        private readonly ContentManager _contentManager;
        private readonly TextureManager _textureManager;
        private GuiSystem _guiSystem;
        private LevelState _levelState;

        public Enums.TurretPart SelectedPart;

        private TurretState _turretState;


        public PlayGuiHandlerSystem(GraphicsDeviceManager graphics, ViewportAdapter viewport,
            GuiSpriteBatchRenderer guiRenderer,
            ContentManager contentManager, TextureManager textureManager, TurretState turretState, LevelState levelState)
        {
            _graphicsDeviceManager = graphics;
            _defaultViewportAdapter = viewport;
            _guiSpriteBatchRenderer = guiRenderer;
            _contentManager = contentManager;
            _textureManager = textureManager;
            _turretState = turretState;
            _levelState = levelState;
        }

        public void Initialize(World world)
        {
            var font = _contentManager.Load<BitmapFont>("KennyBold");
            BitmapFont.UseKernings = false;
            Skin.CreateDefault(font);

            var screen =
                new Screen
                {
                    Content = new Canvas()
                    {
                        Items =
                        {
                            new ProgressBar
                            {
                                Name = "HealthBar",
                                Progress = _levelState.BuildingHealth/ 100,
                                Size = new Size(200, 50),
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 100, 0)
                            },
                            new Label(_levelState.BuildingHealth.ToString("n0"))
                            {
                                Name = "HealthText",
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 35, 0),
                                Size = new Size(70, 50),
                                HorizontalTextAlignment = HorizontalAlignment.Centre,
                                VerticalTextAlignment = VerticalAlignment.Centre
                            }
                        }
                    }
                };

            _guiSystem = new GuiSystem(_defaultViewportAdapter, _guiSpriteBatchRenderer)
            {
                ActiveScreen = screen
            };
        }

        public void Update(GameTime gameTime)
        {
            _guiSystem.ActiveScreen.FindControl<ProgressBar>("HealthBar").Progress = _levelState.BuildingHealth / 100;
            _guiSystem.ActiveScreen.FindControl<Label>("HealthText").Content = _levelState.BuildingHealth.ToString("n0");
            _guiSystem.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            _guiSystem.Draw(gameTime);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}