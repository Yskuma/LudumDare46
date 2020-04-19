using LudumDare46.Levels.Level01;
using LudumDare46.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;
using LudumDare46.Shared.GUI;
using MonoGame.Extended.Gui;

namespace LudumDare46
{
    public class Game1 : Game
    {
        private Texture2D _blackRectangle;

        private ViewportAdapter _boxingViewportAdapter;

        private World _currentLevel;
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TextureManager _textureManager;

        private GUIMain _guiMain;
        private GuiSpriteBatchRenderer _guiSpriteBatchRenderer;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            _spriteBatch.Begin(transformMatrix: _boxingViewportAdapter.GetScaleMatrix());
            _spriteBatch.Draw(_blackRectangle, new Vector2(), null, Color.White, 0f, Vector2.Zero,
                new Vector2(_boxingViewportAdapter.VirtualWidth, _boxingViewportAdapter.VirtualHeight),
                SpriteEffects.None, 0.0f);
            _spriteBatch.End();

            _currentLevel.Draw(gameTime);

            _guiMain.Draw(gameTime);

            base.Draw(gameTime);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //_boxingViewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 1024, 640);

            _boxingViewportAdapter = new DefaultViewportAdapter(GraphicsDevice);
            _graphics.PreferredBackBufferHeight = 640;
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.ApplyChanges();

            _guiSpriteBatchRenderer = new GuiSpriteBatchRenderer(GraphicsDevice, () => Matrix.Identity);

            Window.AllowUserResizing = true;

            _guiMain = new GUIMain(_graphics, _boxingViewportAdapter, _guiSpriteBatchRenderer, Content);

            base.Initialize();

            _currentLevel = new Level01Factory().Build(_graphics, _textureManager, _boxingViewportAdapter, Content);
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureManager = new TextureManager(Content);

            _blackRectangle = new Texture2D(GraphicsDevice, 1, 1);
            _blackRectangle.SetData(new[] {Color.Red});

            _guiMain.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here
            _currentLevel.Update(gameTime);

            _guiMain.Update(gameTime);
            
            base.Update(gameTime);
        }
    }
}