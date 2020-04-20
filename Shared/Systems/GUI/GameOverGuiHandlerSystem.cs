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
    class GameOverGuiHandlerSystem : IUpdateSystem, IDrawSystem
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


        public GameOverGuiHandlerSystem(GraphicsDeviceManager graphics, ViewportAdapter viewport,
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
                            new Label("I'm not sure that's" +
                                      "\r\nhow it happened...")
                            {
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 200, _defaultViewportAdapter.ViewportHeight/2 - 110),
                                Size = new Size(400, 100),
                                HorizontalTextAlignment = HorizontalAlignment.Centre,
                                VerticalTextAlignment = VerticalAlignment.Centre
                            },
                            new Button()
                            {
                                Content = "Restart",
                                Name = "Restart",
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 30, _defaultViewportAdapter.ViewportHeight/2 + 30),
                                Size = new Size(60, 30),
                                HorizontalTextAlignment = HorizontalAlignment.Centre,
                                VerticalTextAlignment = VerticalAlignment.Centre
                            }
                        },
                        BackgroundColor = new Color(30, 30, 30),
                        Size = new Size(_defaultViewportAdapter.ViewportWidth, _defaultViewportAdapter.ViewportHeight)
                    }
                };

            _guiSystem = new GuiSystem(_defaultViewportAdapter, _guiSpriteBatchRenderer)
            {
                ActiveScreen = screen
            };
        }

        public void Update(GameTime gameTime)
        {
            var restartButton = _guiSystem.ActiveScreen.FindControl<Button>("Restart");
            if (restartButton != null && restartButton.IsPressed)
            {
                _levelState.RestartDone = true;
            }

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