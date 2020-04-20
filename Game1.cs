using LudumDare46.Levels;
using LudumDare46.Levels.LevelDefinitions;
using LudumDare46.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Entities;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Gui;

namespace LudumDare46
{
    public class Game1 : Game
    {
        private Texture2D _blackRectangle;

        private ViewportAdapter _boxingViewportAdapter;

        private Level _currentLevel;
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private TextureManager _textureManager;
        private SoundManager _soundManager;

        private LevelDefinition[] _levelDefinitions = {
            new LevelDefinition01(),
            new LevelDefinition02(),
            new LevelDefinition03(),
            new LevelDefinition04(),
            new LevelDefinition05(),
            new LevelDefinition06(),
            new LevelDefinition07(),
            new LevelDefinition08(),
            new LevelDefinition09()
        };

        private int _currentLevelNum = -1;

        //private GuiHandlerSystem _guiMain;
        //private GuiSpriteBatchRenderer _guiSpriteBatchRenderer;

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

            _currentLevel.World.Draw(gameTime);

            //_guiMain.Draw(gameTime);

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

            //_guiSpriteBatchRenderer = new GuiSpriteBatchRenderer(GraphicsDevice, () => Matrix.Identity);

            Window.AllowUserResizing = true;

            

            base.Initialize();

            MediaPlayer.Volume = 0.1f;

            _currentLevel = new LevelMainMenuFactory().Build(_graphics, _textureManager, _soundManager, _boxingViewportAdapter, Content);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _textureManager = new TextureManager(Content);
            _soundManager = new SoundManager(Content);

            _blackRectangle = new Texture2D(GraphicsDevice, 1, 1);
            _blackRectangle.SetData(new[] {Color.Black});

            //_guiMain = new GuiHandlerSystem(_graphics, _boxingViewportAdapter, _guiSpriteBatchRenderer, Content, _textureManager);
            //_guiMain.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (_currentLevel.LevelState.BuildDone)
            {
                var turretState = _currentLevel.TurretState;
                _currentLevel.World.Dispose();
                _currentLevel = new LevelPlayFactory().Build(_graphics, _textureManager, _soundManager, _boxingViewportAdapter, Content, _levelDefinitions[_currentLevelNum], turretState);
            }

            if (_currentLevel.LevelState.RestartDone)
            {
                var turretState = _currentLevel.TurretState;
                _currentLevel.World.Dispose();
                _currentLevel = new LevelBuildFactory().Build(_graphics, _textureManager, _soundManager, _boxingViewportAdapter, Content, _levelDefinitions[_currentLevelNum]);
            }

            if (_currentLevel.LevelState.ContinueDone)
            {
                _currentLevelNum = (_currentLevelNum + 1) % _levelDefinitions.Length;
                _currentLevel.World.Dispose();
                _currentLevel = new LevelBuildFactory().Build(_graphics, _textureManager, _soundManager, _boxingViewportAdapter, Content, _levelDefinitions[_currentLevelNum]);
            }

            // TODO: Add your update logic here
            _currentLevel.World.Update(gameTime);

            //_guiMain.Update(gameTime);
            
            base.Update(gameTime);
        }
    }
}