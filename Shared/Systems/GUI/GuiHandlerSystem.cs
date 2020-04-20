using System;
using System.Collections.Generic;
using System.ComponentModel;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Enums;
using LudumDare46.Shared.Helpers;
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
    class GuiHandlerSystem : IUpdateSystem, IDrawSystem
    {
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly ViewportAdapter _defaultViewportAdapter;
        private readonly GuiSpriteBatchRenderer _guiSpriteBatchRenderer;
        private readonly ContentManager _contentManager;
        private readonly TextureManager _textureManager;
        private GuiSystem _guiSystem;
        private LevelState _levelState;

        private List<ButtonModel> buttonModels;

        public Enums.TurretPart SelectedPart;

        private TurretHelper _turretHelper;

        public GuiHandlerSystem(GraphicsDeviceManager graphics, ViewportAdapter viewport,
            GuiSpriteBatchRenderer guiRenderer,
            ContentManager contentManager, TextureManager textureManager, TurretHelper turretHelper, LevelState levelState)
        {
            _graphicsDeviceManager = graphics;
            _defaultViewportAdapter = viewport;
            _guiSpriteBatchRenderer = guiRenderer;
            _contentManager = contentManager;
            _textureManager = textureManager;
            _turretHelper = turretHelper;
            _levelState = levelState;
        }

        public void Initialize(World world)
        {
            var font = _contentManager.Load<BitmapFont>("KennyBold");
            BitmapFont.UseKernings = false;
            Skin.CreateDefault(font);

            SelectedPart = TurretPart.Empty;

            buttonModels = new List<ButtonModel>()
            {
                new ButtonModel("BarrelEnd",
                    _textureManager.BarrelEnd,
                    "Barrel End" +
                    "\r\n\r\nBullets" +
                    "\r\ncome out of" +
                    "\r\nhere.",
                    Enums.TurretPart.Turret),
                new ButtonModel("BarrelExtender",
                    _textureManager.BarrelExtender,
                    "Extender" +
                    "\r\n\r\nPlace to" +
                    "\r\nright of" +
                    "\r\nbarrel." +
                    "\r\nIncreases" +
                    "\r\ndamage and" +
                    "\r\nreduces" +
                    "\r\nrate of fire.",
                    Enums.TurretPart.BarrelExtender),
                new ButtonModel("BeltFeed",
                    _textureManager.BeltFeed,
                    "Belt Feed" +
                    "\r\n\r\nFeeds ammo" +
                    "\r\nfrom loaders" +
                    "\r\ninto adjacent" +
                    "\r\nbarrel end" +
                    "\r\nor extender.",
                    Enums.TurretPart.BeltFeed),
                new ButtonModel("Loader",
                    _textureManager.Loader,
                    "Ammo Loader" +
                    "\r\n\r\nFeeds ammo" +
                    "\r\nfrom Ammo " +
                    "\r\nBoxes into " +
                    "\r\nadjacent belts " +
                    "\r\nbarrel ends" +
                    "\r\nor extenders." +
                    "\r\nIncreases fire" +
                    "\r\nrate.",
                    Enums.TurretPart.AutoLoader),
                new ButtonModel("AmmoAP",
                    _textureManager.AmmoAP,
                    "AP Ammo Box" +
                    "\r\n\r\nSupplies" +
                    "\r\nArmour piercing" +
                    "\r\nrounds to " +
                    "\r\nloaders. " +
                    "\r\nIncreases" +
                    "\r\nArmour" +
                    "\r\npenetration." +
                    "\r\nIncreases" +
                    "\r\ndamage.",
                    Enums.TurretPart.APAmmo),
                new ButtonModel("AmmoExp", _textureManager.AmmoExp,
                    "Explosive" +
                    "\r\nAmmo Box" +
                    "\r\n\r\nSupplies" +
                    "\r\nExplosive" +
                    "\r\nrounds to " +
                    "\r\nloaders. " +
                    "\r\nIncreases" +
                    "\r\nRadius." +
                    "\r\nDecreases" +
                    "\r\nArmour" +
                    "\r\npenetration.",
                    Enums.TurretPart.ExplosiveAmmo),
                new ButtonModel("AmmoFrag", _textureManager.AmmoFrag,
                    "Fragmentation" +
                    "\r\nAmmo Box" +
                    "\r\n\r\nSupplies" +
                    "\r\nExplosive" +
                    "\r\nrounds to " +
                    "\r\nloaders. " +
                    "\r\nIncreases" +
                    "\r\nRadius." +
                    "\r\nDecreases" +
                    "\r\ndamage.",
                    Enums.TurretPart.FragAmmo),
                new ButtonModel("Remove", _textureManager.Remove,
                    "Remove" +
                    "\r\nTurret" +
                    "\r\nComponent",
                    Enums.TurretPart.Empty)
            };

            var buttons = new UniformGrid()
            {
                Name = "Buttons",
                Columns = 3
            };

            foreach (var buttonModel in buttonModels)
            {
                buttons.Items.Add(new Button()
                {
                    Name = buttonModel.Name,
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
                    Content = new Canvas()
                    {
                        Items =
                        {
                            new DockPanel
                            {
                                Name = "DemoList",
                                AttachedProperties = {{DockPanel.DockProperty, Dock.Right}},
                                VerticalAlignment = VerticalAlignment.Top,
                                HorizontalAlignment = HorizontalAlignment.Left,
                                BackgroundColor = new Color(30, 30, 30),
                                LastChildFill = true,
                                Size = new Size(300, _defaultViewportAdapter.ViewportHeight),
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

                                            buttons,
                                            new TextBox()
                                            {
                                                Name = "Description",
                                                VerticalTextAlignment = VerticalAlignment.Top,
                                                Size = new Size(280, _defaultViewportAdapter.ViewportHeight)
                                            }
                                        }
                                    }
                                }
                            },
                            new Button()
                            {
                                Content = "Continue",
                                Size = new Size(200, 50),
                                Position = new Point(_defaultViewportAdapter.ViewportWidth / 2 - 100,
                                    _defaultViewportAdapter.ViewportHeight - 50)
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
            public ButtonModel(string name, TextureRegion2D texture, string description, Enums.TurretPart part)
            {
                Name = name;
                Texture = texture;
                Description = description;
                Part = part;
            }

            public string Name { get; }
            public TextureRegion2D Texture { get; }

            public string Description { get; }
            public Enums.TurretPart Part { get; }

            public override string ToString()
            {
                return Name;
            }
        }

        public void Update(GameTime gameTime)
        {
            _guiSystem.Update(gameTime);

            var mouseState = MouseExtended.GetState();

            foreach (var buttonModel in buttonModels)
            {
                var button = _guiSystem.ActiveScreen.FindControl<Button>(buttonModel.Name);
                if (button.IsPressed)
                {
                    var description = _guiSystem.ActiveScreen.FindControl<TextBox>("Description");
                    description.Text = buttonModel.Description;
                    SelectedPart = buttonModel.Part;
                }
            }

            var endButton = _guiSystem.ActiveScreen.FindControl<Button>("EndBuild");
            if (endButton != null && endButton.IsPressed)
            {
                _levelState.BuildDone = true;
            }

            if (mouseState.WasButtonJustDown(MouseButton.Right))
            {
                SelectedPart = TurretPart.Empty;
            }

            if (mouseState.WasButtonJustDown(MouseButton.Left))
            {
                PlacePartAtMouse(SelectedPart);
            }
        }

        public void PlacePartAtMouse(TurretPart part)
        {
            var mouseState = MouseExtended.GetState();
            var currentTile = new Point()
            {
                X = (int) Math.Round((float) mouseState.Position.X / 16),
                Y = (int) Math.Round((float) mouseState.Position.Y / 16)
            };
            _turretHelper.AddPart(currentTile, SelectedPart);
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