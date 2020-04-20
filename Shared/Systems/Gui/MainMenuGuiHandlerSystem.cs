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
    class MainMenuGuiHandlerSystem : IUpdateSystem, IDrawSystem
    {
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly ViewportAdapter _defaultViewportAdapter;
        private readonly GuiSpriteBatchRenderer _guiSpriteBatchRenderer;
        private readonly ContentManager _contentManager;
        private readonly TextureManager _textureManager;
        private GuiSystem _guiSystem;
        private LevelState _levelState;
        private Screen _mainScreen;

        public MainMenuGuiHandlerSystem(GraphicsDeviceManager graphics, ViewportAdapter viewport,
            GuiSpriteBatchRenderer guiRenderer,
            ContentManager contentManager, TextureManager textureManager, LevelState levelState)
        {
            _graphicsDeviceManager = graphics;
            _defaultViewportAdapter = viewport;
            _guiSpriteBatchRenderer = guiRenderer;
            _contentManager = contentManager;
            _textureManager = textureManager;
            _levelState = levelState;
        }

        public void Initialize(World world)
        {
            var font = _contentManager.Load<BitmapFont>("KennyBold");
            BitmapFont.UseKernings = false;
            Skin.CreateDefault(font);
            _mainScreen =
                new Screen
                {
                    Content = new Canvas()
                    {
                        Items =
                        {
                            new Label("Defend the Pig")
                            {
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 300, _defaultViewportAdapter.ViewportHeight/2 - 210),
                                Size = new Size(600, 50),
                                HorizontalTextAlignment = HorizontalAlignment.Centre,
                                VerticalTextAlignment = VerticalAlignment.Centre
                            },
                            new Label("The future depends on this pig.")
                            {
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 300, _defaultViewportAdapter.ViewportHeight/2 - 110),
                                Size = new Size(600, 50),
                                HorizontalTextAlignment = HorizontalAlignment.Centre,
                                VerticalTextAlignment = VerticalAlignment.Centre
                            },
                            new Label("Defend it at all costs...")
                            {
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 300, _defaultViewportAdapter.ViewportHeight/2 - 60),
                                Size = new Size(600, 50),
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
                ActiveScreen = _mainScreen
            };
        }

        public void Update(GameTime gameTime)
        {
            var continueButton = _guiSystem.ActiveScreen.FindControl<Button>("Continue");
            if (continueButton != null && continueButton.IsPressed)
                {
                    _levelState.ContinueDone = true;
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