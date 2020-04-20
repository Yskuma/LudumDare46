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
        private Screen _healthScreen;
        private Screen _gameOverScreen;
        private Screen _gameWinScreen;

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

            var _healthScreen =
                new Screen
                {
                    Content = new Canvas()
                    {
                        Items =
                        {
                            new Label("Level")
                            {
                                Content = _levelState.Name,
                                Size = new Size(200, 50),
                                Position = new Point(_defaultViewportAdapter.ViewportWidth - 200, 10),
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                VerticalAlignment = VerticalAlignment.Top,
                                HorizontalTextAlignment = HorizontalAlignment.Centre,
                                VerticalTextAlignment = VerticalAlignment.Top
                            },
                            new ProgressBar
                            {
                                Name = "HealthBar",
                                Progress = _levelState.BuildingHealth/ 100,
                                Size = new Size(500, 50),
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 250, 10)
                            },
                            new Label(_levelState.BuildingHealth.ToString("n0"))
                            {
                                Name = "HealthText",
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 35, 10),
                                Size = new Size(70, 50),
                                HorizontalTextAlignment = HorizontalAlignment.Centre,
                                VerticalTextAlignment = VerticalAlignment.Centre
                            }
                        }
                    }
                };

            _gameOverScreen =
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
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 80, _defaultViewportAdapter.ViewportHeight/2 + 30),
                                Size = new Size(160, 50),
                                HorizontalTextAlignment = HorizontalAlignment.Centre,
                                VerticalTextAlignment = VerticalAlignment.Centre
                            }
                        },
                        BackgroundColor = new Color(30, 30, 30),
                        Size = new Size(_defaultViewportAdapter.ViewportWidth, _defaultViewportAdapter.ViewportHeight)
                    }
                };

            _gameWinScreen =
                new Screen
                {
                    Content = new Canvas()
                    {
                        Items =
                        {
                            new Label("We defended the pig" +
                                      "\r\ntime to move it to a" + 
                                      "\r\nnew location...")
                            {
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 200, _defaultViewportAdapter.ViewportHeight/2 - 110),
                                Size = new Size(400, 100),
                                HorizontalTextAlignment = HorizontalAlignment.Centre,
                                VerticalTextAlignment = VerticalAlignment.Centre
                            },
                            new Button()
                            {
                                Content = "Continue",
                                Name = "Continue",
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 80, _defaultViewportAdapter.ViewportHeight/2 + 30),
                                Size = new Size(160, 50),
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
                ActiveScreen = _healthScreen
            };
        }

        public void Update(GameTime gameTime)
        {
            var healthBar = _guiSystem.ActiveScreen.FindControl<ProgressBar>("HealthBar");
            if (healthBar != null)
            {
                healthBar.Progress = _levelState.BuildingHealth / 10;
            }

            var healthtext = _guiSystem.ActiveScreen.FindControl<Label>("HealthText");
            if (healthtext != null)
            {
                healthtext.Content = _levelState.BuildingHealth.ToString("n0");
            }

            var restartButton = _guiSystem.ActiveScreen.FindControl<Button>("Restart");
            if (restartButton != null)
            {
                if (restartButton != null && restartButton.IsPressed)
                {
                    _levelState.RestartDone = true;
                }
            }

            var continueButton = _guiSystem.ActiveScreen.FindControl<Button>("Continue");
            if (continueButton != null && continueButton.IsPressed)
                {
                    _levelState.ContinueDone = true;
                }
            

            if (_levelState.GameOver)
            {
                _guiSystem.ActiveScreen = _gameOverScreen;
            }

            if (_levelState.GameWon)
            {
                _guiSystem.ActiveScreen = _gameWinScreen;
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