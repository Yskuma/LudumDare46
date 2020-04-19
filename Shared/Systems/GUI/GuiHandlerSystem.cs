using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.Systems.Gui
{
    class GuiHandlerSystem : IUpdateSystem, IDrawSystem
    {
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly ViewportAdapter _defaultViewportAdapter;
        private readonly GuiSpriteBatchRenderer _guiSpriteBatchRenderer;
        private readonly ContentManager _contentManager;
        private readonly TextureManager _textureManager;
        private GuiSystem _guiSystem;

        public GuiHandlerSystem(GraphicsDeviceManager graphics, ViewportAdapter viewport, GuiSpriteBatchRenderer guiRenderer,
            ContentManager contentManager, TextureManager textureManager)
        {
            _graphicsDeviceManager = graphics;
            _defaultViewportAdapter = viewport;
            _guiSpriteBatchRenderer = guiRenderer;
            _contentManager = contentManager;
            _textureManager = textureManager;
        }

        public void Initialize(World world)
        {
            var font = _contentManager.Load<BitmapFont>("KennyBold");
            BitmapFont.UseKernings = false;
            Skin.CreateDefault(font);

            var buttonModels = new List<ButtonModel>()
            {
                new ButtonModel("BarrelEnd", _textureManager.BarrelEnd),
                new ButtonModel("BarrelExtender", _textureManager.BarrelExtender),
                new ButtonModel("BeltFeed", _textureManager.BeltFeed),
                new ButtonModel("Loader", _textureManager.Loader),
                new ButtonModel("AmmoAP", _textureManager.AmmoAP),
                new ButtonModel("AmmoExp", _textureManager.AmmoExp),
                new ButtonModel("AmmoFrag", _textureManager.AmmoFrag),
            };

            var buttons = new UniformGrid()
            {
                Columns = 3
            };

            foreach (var buttonModel in buttonModels)
            {
                buttons.Items.Add(new Button
                {
                    Size = new Size(64,
                        64),
                    Margin = 5,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    BackgroundRegion = buttonModel.Texture,
                    BackgroundColor = Color.White
                });
            }

            var Screen =
                new Screen
                {
                    Content = new DockPanel
                    {
                        Name = "DemoList",
                        AttachedProperties = {{DockPanel.DockProperty, Dock.Right}},
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        BackgroundColor = new Color(30, 30, 30),
                        LastChildFill = true,
                        Items =
                        {
                            new StackPanel()
                            {
                                Items =
                                {
                                    new Label("Build Menu")
                                    {
                                        Content = "Build Menu",
                                        Margin = 5,
                                        HorizontalAlignment = HorizontalAlignment.Stretch,
                                        VerticalAlignment = VerticalAlignment.Top,
                                        HorizontalTextAlignment = HorizontalAlignment.Centre,
                                        VerticalTextAlignment = VerticalAlignment.Top
                                    },

                                    buttons
                                }
                            }
                        }
                    }
                };

            _guiSystem = new GuiSystem(_defaultViewportAdapter, _guiSpriteBatchRenderer)
            {
                ActiveScreen = Screen
            };

        }

        public class ButtonModel
        {
            public ButtonModel(string name, TextureRegion2D texture)
            {
                Name = name;
                Texture = texture;
            }

            public string Name { get; }
            public TextureRegion2D Texture { get; }

            public override string ToString()
            {
                return Name;
            }
        }

        public void Update(GameTime gameTime)
        {
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