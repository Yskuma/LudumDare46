using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Gui;
using MonoGame.Extended.Gui.Controls;
using MonoGame.Extended.Gui.Markup;
using MonoGame.Extended.Gui.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGame.Extended.ViewportAdapters;

namespace LudumDare46.Shared.GUI
{
    class GUIMain
    {
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly ViewportAdapter _defaultViewportAdapter;
        private readonly GuiSpriteBatchRenderer _guiSpriteBatchRenderer;
        private readonly ContentManager _contentManager;
        private readonly TextureManager _textureManager;
        private GuiSystem _guiSystem;

        public GUIMain(GraphicsDeviceManager graphics, ViewportAdapter viewport, GuiSpriteBatchRenderer guiRenderer,
            ContentManager contentManager, TextureManager textureManager)
        {
            _graphicsDeviceManager = graphics;
            _defaultViewportAdapter = viewport;
            _guiSpriteBatchRenderer = guiRenderer;
            _contentManager = contentManager;
            _textureManager = textureManager;
        }

        public void LoadContent()
        {
            var font = _contentManager.Load<BitmapFont>("KennyBold");
            BitmapFont.UseKernings = false;
            Skin.CreateDefault(font);

            //var parser = new MarkupParser();

            //var mainScreen = new Screen
            //{
            //    Content = parser.Parse("Features/MainWindow.mgeml", new object())
            //};

            //var textBox = mainScreen.FindControl<TextBox>("TextBox");
            //var statusLabel = mainScreen.FindControl<Label>("StatusLabel");

            //textBox.CaretIndexChanged += (sender, args) =>
            //    statusLabel.Content = $"Ln {textBox.LineIndex + 1}, Ch {textBox.CaretIndex + 1}";

            var Screen =
                new Screen
                {
                    Content = new DockPanel
                    {
                        Name = "DemoList",
                        AttachedProperties = {{DockPanel.DockProperty, Dock.Left}},
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

                                    new UniformGrid
                                    {
                                        
                                        Columns = 3,
                                        
                                        Items =
                                        {
                                            new Button
                                            {
                                                Size = new Size(64,
                                                    64),
                                                Margin = 5,

                                                HorizontalAlignment = HorizontalAlignment.Left,
                                                VerticalAlignment = VerticalAlignment.Top
                                            },
                                            new Button
                                            {
                                                Size = new Size(64,
                                                    64),
                                                Margin = 5,
                                                HorizontalAlignment = HorizontalAlignment.Left,
                                                VerticalAlignment = VerticalAlignment.Top
                                            },
                                            new Button
                                            {
                                                Size = new Size(64,
                                                    64),
                                                Margin = 5,
                                                HorizontalAlignment = HorizontalAlignment.Left,
                                                VerticalAlignment = VerticalAlignment.Top
                                            },
                                            new Button
                                            {
                                                Size = new Size(64,
                                                    64),
                                                Margin = 5,
                                                HorizontalAlignment = HorizontalAlignment.Left,
                                                VerticalAlignment = VerticalAlignment.Top
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };

            _guiSystem = new GuiSystem(_defaultViewportAdapter, _guiSpriteBatchRenderer)
            {
                ActiveScreen = Screen
            };

            //var demoList = demoScreen.FindControl<ListBox>("DemoList");
            var demoContent = Screen.FindControl<ContentControl>("Content");

            //demoList.SelectedIndexChanged += (sender, args) => demoContent.Content = (demoList.SelectedItem as DemoViewModel)?.Content;
            //demoContent.Content = (demoList.SelectedItem as DemoViewModel)?.Content;
        }

        public class ViewModel
        {
            public ViewModel(string name, object content)
            {
                Name = name;
                Content = content;
            }

            public string Name { get; }
            public object Content { get; }

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
    }
}