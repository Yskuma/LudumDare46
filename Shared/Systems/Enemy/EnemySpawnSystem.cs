using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LudumDare46.Enemy;
using LudumDare46.Shared.Components;
using LudumDare46.Shared.Components.EnemyComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Entities.Systems;
using MonoGame.Extended.Sprites;

namespace LudumDare46.Shared.Systems
{
    class EnemySpawnSystem : EntityUpdateSystem
    {
        private readonly TextureManager _textureManager;
        private readonly LevelState _levelState;

        private double _currentTime = 0;

        private List<Rectangle> _spawnAreas;
        private readonly List<EnemySpawnItem> _spawnList;

        //private EnemyFactory _enemyFactory;
        private Random _random;

        private Dictionary<EnemyAppearance, Texture2D> _enemyTexDict = new Dictionary<EnemyAppearance, Texture2D>();

        public EnemySpawnSystem(TextureManager textureManager, LevelState levelState, List<Rectangle> spawnAreas, List<EnemySpawnItem> spawnList) : base(new AspectBuilder())
        {
            _textureManager = textureManager;
            _levelState = levelState;
            _spawnAreas = spawnAreas;
            _spawnList = spawnList.OrderBy(r => r.SpawnTime).ToList();

            _random = new Random(1983);

            _enemyTexDict.Add(EnemyAppearance.Man, _textureManager.SciFiUnit01);
            _enemyTexDict.Add(EnemyAppearance.Car, _textureManager.SciFiUnit06);
            _enemyTexDict.Add(EnemyAppearance.Truck, _textureManager.SciFiUnit07);
            _enemyTexDict.Add(EnemyAppearance.APC, _textureManager.SciFiUnit10);
            _enemyTexDict.Add(EnemyAppearance.Tank, _textureManager.SciFiUnit09);
        }

        public override void Update(GameTime gameTime)
        {
            _currentTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (_spawnList.Any() && _spawnList[0].SpawnTime <= _currentTime)
            {
                var spawnEnemy = _spawnList[0].Enemy;

                var spawnIndex = _random.Next(_spawnAreas.Count);
                var spawnX = (float) (_random.NextDouble() * _spawnAreas[spawnIndex].Width) + _spawnAreas[spawnIndex].X;
                var spawnY = (float) (_random.NextDouble() * _spawnAreas[spawnIndex].Height) +
                             _spawnAreas[spawnIndex].Y;

                var e = CreateEntity();
                e.Attach(new Transform2(spawnX, spawnY, 0.0F, 1.0F, 1.0F));
                e.Attach(new MovementComponent(Vector2.UnitX * spawnEnemy.Speed));
                e.Attach(new EnemyComponent()
                {
                    HP = spawnEnemy.HP,
                    Armour = spawnEnemy.Armour,
                    Speed = spawnEnemy.Speed,
                    Damage = spawnEnemy.Damage
                });
                e.Attach(new Sprite(_enemyTexDict[spawnEnemy.Appearance]));

                _spawnList.RemoveAt(0);
            }

            _levelState.SpawnsRemaining = _spawnList.Count;
        }

        public override void Initialize(IComponentMapperService mapperService)
        {
        }
    }
}